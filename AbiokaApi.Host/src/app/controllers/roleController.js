(function () {
    'use strict';

    angular.module('abioka')
      .controller('RoleController', RoleController);

    /* @ngInject */
    function RoleController($timeout, $mdDialog, AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.roles,
            query: {},
            columns: [{ name: "Name", text: "Name", order: true }]
        };
        vm.showDialog = showDialog;
        vm.showDeleteDialog = showDeleteDialog;

        function showDialog(event, entity) {
            return showEditOrDeleteDialog(event, entity, '/templates/admin/roleDialog.html', false);
        }

        function showDeleteDialog(event, entity) {
            return showEditOrDeleteDialog(event, entity, '/templates/shared/deleteComponent.html', true);
        }

        function showEditOrDeleteDialog(ev, entity, template, isDelete) {
            return $mdDialog.show({
                controller: 'RoleDialogController',
                controllerAs: 'vm',
                templateUrl: template,
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                locals: {
                    entity: entity
                }
            });
        }
    }
})();
