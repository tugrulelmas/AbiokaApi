(function () {
    'use strict';

    var menus = {
        templateUrl: '/app/components/menu/menus.html',
        controller: MenusController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function MenusController($filter, AdminResource) {
        var vm = this;

        var fulldateFormat = $filter("translate")("FullDateFormat");

        vm.options = {
            loadOnInit: true,
            rowSelection: false,
            resource: AdminResource.menus,
            query: {},
            columns: [{ name: "Text", text: "Text", order: true, filter: "translate" },
                      { name: "Parent.Text", text: "Parent", order: true, filter: "translate" },
                      { name: "Url", text: "Url", order: true },
                      { name: "Order", text: "Order", order: true, },
                      { name: "Role.Name", text: "Role", order: true },
                      { name: "CreatedDate", text: "CreatedDate", order: true, cellTemplate: "<span>{{entity.CreatedDate | abDate:'" + fulldateFormat + "'}}</span>" },
                      { name: "UpdatedDate", text: "UpdatedDate", order: true, cellTemplate: "<span>{{entity.UpdatedDate | abDate:'" + fulldateFormat + "'}}</span>" }],
            dialogController: 'MenuDialogController',
            editTemplate: '/app/components/menu/menuDialog.html',
            deleteTemplate: '/app/shared/deleteComponent/deleteComponent.html'
        };
    }

    angular.module('abioka')
      .component('menus', menus)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('menus', {
                url: '/menus',
                template: '<menus></menus>',
                isPublic: false
            });
    }
})();
