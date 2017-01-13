(function () {
    'use strict';

    var roles = {
        templateUrl: '/app/components/role/roles.html',
        controller: RolesController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function RolesController($filter, AdminResource) {
        var vm = this;

        var fulldateFormat = $filter("translate")("FullDateFormat");

        vm.options = {
            loadOnInit: true,
            rowSelection: false,
            resource: AdminResource.roles,
            query: {},
            columns: [{ name: "Name", text: "Name", order: true },
                      { name: "CreatedDate", text: "CreatedDate", order: true, cellTemplate: "<span>{{entity.CreatedDate | abDate:'" + fulldateFormat + "'}}</span>" },
                      { name: "UpdatedDate", text: "UpdatedDate", order: true, cellTemplate: "<span>{{entity.UpdatedDate | abDate:'" + fulldateFormat + "'}}</span>" }],
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
