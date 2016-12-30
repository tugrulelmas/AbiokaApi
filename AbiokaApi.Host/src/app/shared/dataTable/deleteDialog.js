(function () {
    'use strict';

    var deleteDialog = {
        bindings: {
            title: '@',
            message: '@',
            resource: '<',
            entity: '<'
        },
        templateUrl: '/app/shared/dataTable/deleteDialog.html',
        controller: DeleteDialogController,
    };

    /* @ngInject */
    function DeleteDialogController($scope, $mdDialog, alert) {
        var self = this;
        self.cancel = cancel;
        self.loading = false;
        self.deleteEntity = deleteEntity;

        function deleteEntity() {
            self.loading = true;
            self.resource.delete({ id: self.entity.Id }).$promise.then(function () {
                self.loading = false;
                $mdDialog.hide(self.entity);
                alert.success("ItemIsDeleted", true);
            }, function () { });
        }

        function cancel() {
            $mdDialog.cancel();
        }

        $scope.$on("errorOccurred", function (event, data) {
            vm.loading = false;
        });
    }

    angular.module('abioka')
      .component('deleteDialog', deleteDialog);
})();
