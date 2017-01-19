(function () {
    'use strict';

    var users = {
        templateUrl: '/app/components/user/users.html',
        controller: UsersController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function UsersController($timeout, $filter, AdminResource) {
        var vm = this;
        vm.loadData = false;

        var fulldateFormat = $filter("translate")("FullDateFormat");

        vm.options = {
            loadOnInit: false,
            rowSelection: false,
            resource: AdminResource.users,
            query: {},
            columns: [{ name: "Email", text: "Email", order: true },
                      { name: "Name", text: "Name", order: true },
                      { name: "Surname", text: "Surname", order: true },
                      { name: "Gender", text: "Gender", order: true, filter: "translate" },
                      { name: "CreatedDate", text: "CreatedDate", order: true, cellTemplate: "<span>{{entity.CreatedDate | abDate:'" + fulldateFormat + "'}}</span>" },
                      { name: "UpdatedDate", text: "UpdatedDate", order: true, cellTemplate: "<span>{{entity.UpdatedDate | abDate:'" + fulldateFormat + "'}}</span>" }],
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
