(function () {
    'use strict';

    angular.module('abioka')
      .directive('editDialog', editDialog);

    function editDialog() {
        var directive = {
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: {
                title: '@',
                resource: '=',
                entity: '='
            },
            templateUrl: '/templates/shared/editDialog.html',
            controller: editDialogController,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    /* @ngInject */
    function editDialogController($scope, $mdDialog) {
        var vm = this;
        vm.cancel = cancel;
        vm.loading = false;
        vm.saveDialog = saveDialog;

        function saveDialog() {
            vm.loading = true;
            if (vm.entity && vm.entity.Id) {
                vm.resource.update({}, vm.entity).$promise.then(closeDialog);
            } else {
                vm.resource.save({}, vm.entity).$promise.then(closeDialog);
            }
        }

        function closeDialog() {
            vm.loading = false;
            $mdDialog.hide(vm.entity);
        }

        function cancel() {
            $mdDialog.cancel();
        }

        $scope.$on("errorOccurred", function (event, data) {
            vm.loading = false;
        });
    }
})();
