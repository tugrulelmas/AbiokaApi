﻿(function () {
    'use strict';

    var abiokaDataTable = {
        bindings: {
            options: '<',
            showDialog: '&',
            showDeleteDialog: '&'
        },
        templateUrl: '/templates/shared/dataTable.html',
        controller: dataTableController
    }

    /* @ngInject */
    function dataTableController($timeout, $filter, $mdDialog) {
        var self = this;

        this.$onInit = function () {
            var defaultQuery = { order: ' ', limit: 10, page: 1 };
            angular.extend(defaultQuery, self.options.query);
            self.query = defaultQuery;
            self.entities = {};
            self.selected = [];
            self.getData = getData;
            self.promise = $timeout(function () { }, 500);
            self.showEditDialog = showEditDialog;
            self.showCustomDeleteDialog = showCustomDeleteDialog;
            self.pageLabel = {
                page: $filter("translate")('Page') + ":",
                rowsPerPage: $filter("translate")('RowsPerPage') + ':',
                of: $filter("translate")('of')
            };
            if (self.options.loadData) {
                getData();
            }
        };

        this.$onChanges = function (changesObj) {
            if (changesObj.options.loadData) {
                getData();
            }
        }

        function getData() {
            self.promise = self.options.resource.get(self.query, success).$promise;
        }

        function success(data) {
            self.entities = data;
        }

        function showEditDialog(event, entity) {
            if (entity && entity.Id) {
                self.options.resource.get({ id: entity.Id }, function (data) {
                    showEditOrDeleteDialog(event, data, self.options.editTemplate).then(function (updatedEntity) {
                        angular.copy(updatedEntity, entity);
                    });
                });
            } else {
                showEditOrDeleteDialog(event, null, self.options.editTemplate).then(function (updatedEntity) {
                    self.entities.Data.push(updatedEntity);
                    self.entities.Count += 1;
                });
            }
        }

        function showCustomDeleteDialog(event, entity) {
            var tmpEntity = angular.copy(entity);
            showEditOrDeleteDialog(event, tmpEntity, self.options.deleteTemplate).then(function (deletedEntity) {
                self.entities.Data.splice(self.entities.Data.indexOf(entity), 1);
                self.entities.Count -= 1;
            });
        }

        function showEditOrDeleteDialog(event, entity, template) {
            return $mdDialog.show({
                controller: self.options.dialogController,
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
    }

    angular.module('abioka')
      .component('abiokaDataTable', abiokaDataTable);
})();
