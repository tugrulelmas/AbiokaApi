(function () {
    'use strict';

    angular.module('abioka')
      .directive('deleteDialog', deleteDialog);

    /* @ngInject */
    function deleteDialog($mdDialog) {
        var directive = {
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: {
                title: '@',
                message: '@',
                resource: '=',
                entity: '='
            },
            templateUrl: '/templates/shared/deleteDialog.html',
            controller: deleteDialogController,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;

        function deleteDialogController() {
            var vm = this;
            vm.cancel = cancel;
            vm.loading = false;
            vm.deleteEntity = deleteEntity;

            function deleteEntity() {
                vm.loading = true;
                vm.resource.delete({ id: vm.entity.Id }).$promise.then(function () {
                    vm.loading = false;
                    $mdDialog.hide(vm.entity);
                });
            }

            function cancel() {
                $mdDialog.cancel();
            }
        }
    }
})();
