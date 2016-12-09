(function () {
    'use strict';

    angular.module('abioka')
      .directive('abiokaDataTable', abiokaDataTable);

    /* @ngInject */
    function abiokaDataTable($timeout) {
        var directive = {
            restrict: 'E',
            //transclude: true,
            scope: {
                options: '=',
                showDialog: '&',
                showDeleteDialog: '&'
            },
            templateUrl: '/templates/shared/dataTable.html',
            controller: dataTableController,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;

        function dataTableController() {
            var vm = this;
            var defaultQuery = { order: 'Name', limit: 10, page: 1 };
            angular.extend(defaultQuery, vm.options.query);
            vm.query = defaultQuery;
            vm.entities = {};
            vm.selected = [];
            vm.getData = getData;
            vm.promise = $timeout(function () { }, 500);
            vm.showEditDialog = showEditDialog;
            vm.showCustomDeleteDialog = showCustomDeleteDialog;

            function getData() {
                vm.promise = vm.options.resource.get(vm.query, success).$promise;
            }

            function success(data) {
                vm.entities = data;
            }

            function showEditDialog(event, entity) {
                var tmpEntity = angular.copy(entity);
                vm.showDialog({ event: event, entity: tmpEntity }).then(function (updatedEntity) {
                    angular.copy(updatedEntity, entity); 
                });
            }

            function showCustomDeleteDialog(event, entity) {
                var tmpEntity = angular.copy(entity);
                vm.showDeleteDialog({ event: event, entity: tmpEntity }).then(function (deletedEntity) {
                    vm.entities.Data.splice(vm.entities.Data.indexOf(entity), 1);
                });
            }
        }
    }
})();
