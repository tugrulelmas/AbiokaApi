(function () {
    'use strict';

    angular.module('abioka')
      .directive('abiokaDataTable', abiokaDataTable);

    function abiokaDataTable() {
        var directive = {
            restrict: 'E',
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
    }

    /* @ngInject */
    function dataTableController($scope, $timeout, $filter, $mdDialog) {
        var vm = this;
        var defaultQuery = { order: ' ', limit: 10, page: 1 };
        angular.extend(defaultQuery, vm.options.query);
        vm.query = defaultQuery;
        vm.entities = {};
        vm.selected = [];
        vm.getData = getData;
        vm.promise = $timeout(function () { }, 500);
        vm.showEditDialog = showEditDialog;
        vm.showCustomDeleteDialog = showCustomDeleteDialog;
        vm.pageLabel = {
            page: $filter("translate")('Page') + ":",
            rowsPerPage: $filter("translate")('RowsPerPage') + ':',
            of: $filter("translate")('of')
        };

        function getData() {
            vm.promise = vm.options.resource.get(vm.query, success).$promise;
        }

        function success(data) {
            vm.entities = data;
        }

        function showEditDialog(event, entity) {
            if (entity && entity.Id) {
                vm.options.resource.get({ id: entity.Id }, function (data) {
                    showEditOrDeleteDialog(event, data, vm.options.editTemplate).then(function (updatedEntity) {
                        angular.copy(updatedEntity, entity);
                    });
                });
            } else {
                showEditOrDeleteDialog(event, null, vm.options.editTemplate).then(function (updatedEntity) {
                    vm.entities.Data.push(updatedEntity);
                    vm.entities.Count += 1;
                });
            }
        }

        function showCustomDeleteDialog(event, entity) {
            var tmpEntity = angular.copy(entity);
            showEditOrDeleteDialog(event, tmpEntity, vm.options.deleteTemplate).then(function (deletedEntity) {
                vm.entities.Data.splice(vm.entities.Data.indexOf(entity), 1);
                vm.entities.Count -= 1;
            });
        }

        function showEditOrDeleteDialog(event, entity, template) {
            return $mdDialog.show({
                controller: vm.options.dialogController,
                controllerAs: 'vm',
                templateUrl: template,
                parent: angular.element(document.body),
                targetEvent: event,
                clickOutsideToClose: false,
                locals: {
                    entity: entity
                }
            });
        }

        $scope.$watch("vm.options.loadData", function (newVal) {
            if (newVal) {
                getData();
            }
        });
    }
})();
