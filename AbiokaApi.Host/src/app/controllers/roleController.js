(function () {
    'use strict';

    angular.module('abioka')
      .controller('RoleController', RoleController);

    /* @ngInject */
    function RoleController($timeout, AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.roles,
            query: {},
            columns: [{ name: "Name", text: "Name", order: true }],
            dialogController: 'RoleDialogController',
            editTemplate: '/templates/admin/roleDialog.html',
            deleteTemplate: '/templates/shared/deleteComponent.html'
        };
    }
})();
