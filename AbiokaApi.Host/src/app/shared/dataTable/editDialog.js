(function () {
    'use strict';

    var editDialog = {
        bindings: {
            title: '@',
            resource: '<',
            entity: '<',
            form: '@'
        },
        templateUrl: '/app/shared/dataTable/editDialog.html',
        controller: editDialogController,
        transclude: true
    }

    /* @ngInject */
    function editDialogController($scope, $mdDialog, alert) {
        var self = this;
        self.cancel = cancel;
        self.loading = false;
        self.saveDialog = saveDialog;
        self.dialogForm = $scope.$parent[self.form];

        function saveDialog() {
            if (self.dialogForm && !self.dialogForm.$valid)
                return;

            self.loading = true;
            if (self.entity && self.entity.Id) {
                self.resource.update({}, self.entity).$promise.then(function (data) {
                    closeDialog(data);
                    alert.success("ItemIsUpdated", true);
                });
            } else {
                self.resource.save({}, self.entity).$promise.then(function (data) {
                    closeDialog(data);
                    alert.success("ItemIsSaved", true);
                });
            }
        }

        function closeDialog(data) {
            if (data && data.Id) {
                self.entity.Id = data.Id;
            }
            self.loading = false;
            $mdDialog.hide(self.entity);
        }

        function cancel() {
            $mdDialog.cancel();
        }

        $scope.$on("errorOccurred", function (event, data) {
            self.loading = false;
        });
    }

    angular.module('abioka')
      .component('editDialog', editDialog);
})();
