(function () {
    'use strict';

    var users = {
        templateUrl: '/app/components/user/users.html',
        controller: UsersController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function UsersController($timeout, AdminResource) {
        var vm = this;
        vm.loadData = false;
        vm.options = {
            loadOnInit: false,
            rowSelection: false,
            resource: AdminResource.users,
            query: {},
            columns: [{ name: "Email", text: "Email", order: true }],
            dialogController: 'UserDialogController',
            editTemplate: '/app/components/user/userDialog.html',
            deleteTemplate: '/app/shared/deleteComponent/deleteComponent.html'
        };

        $timeout(function () {
            vm.loadData = true;
        }, 1000);
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
