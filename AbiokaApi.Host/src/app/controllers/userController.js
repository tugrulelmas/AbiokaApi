(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserController', UserController);

    /* @ngInject */
    function UserController($timeout, AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.users,
            query: {},
            columns: [{ name: "Email", text: "Email", order: true }],
            dialogController: 'UserDialogController',
            editTemplate: '/templates/admin/userDialog.html',
            deleteTemplate: '/templates/shared/deleteComponent.html'
        };
    }
})();
