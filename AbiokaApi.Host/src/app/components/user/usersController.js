(function () {
    'use strict';

    var users = {
        templateUrl: '/app/components/user/users.html',
        controller: UsersController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function UsersController(AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.users,
            query: {},
            columns: [{ name: "Email", text: "Email", order: true }],
            dialogController: 'UserDialogController',
            editTemplate: '/app/components/user/userDialog.html',
            deleteTemplate: '/app/shared/deleteComponent/deleteComponent.html'
        };
    }

    angular.module('abioka')
      .component('users', users)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('users', {
                url: '/users',
                template: '<users></users>',
                isPublic: false
            });
    }
})();
