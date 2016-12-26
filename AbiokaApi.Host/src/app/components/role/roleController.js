(function () {
    'use strict';

    var roles = {
        templateUrl: '/app/components/role/roles.html',
        controller: RolesController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function RolesController(AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.roles,
            query: {},
            columns: [{ name: "Name", text: "Name", order: true }],
            dialogController: 'RoleDialogController',
            editTemplate: '/app/components/role/roleDialog.html',
            deleteTemplate: '/app/shared/deleteComponent/deleteComponent.html'
        };
    }

    angular.module('abioka')
      .component('roles', roles)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('roles', {
                url: '/roles',
                template: '<roles></roles>',
                isPublic: false
            });
    }
})();
