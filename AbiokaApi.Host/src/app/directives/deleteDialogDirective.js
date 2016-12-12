(function () {
    'use strict';

    angular.module('abioka')
      .directive('deleteDialog', deleteDialog);

    function deleteDialog() {
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
    }

    /* @ngInject */
    function deleteDialogController($scope, $mdDialog) {
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

        $scope.$on("errorOccurred", function (event, data) {
            vm.loading = false;
        });
    }
})();
