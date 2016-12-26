(function () {
    'use strict';

    angular.module('abioka')
      .controller('RoleDialogController', RoleDialogController);

    /* @ngInject */
    function RoleDialogController($filter, AdminResource, entity) {
        var vm = this;
        vm.resource = AdminResource.roles;
        vm.entity = entity;
        vm.title = $filter("translate")("RoleDetail");
        vm.deleteTitle = $filter("translate")("DeleteRole");
        vm.deleteMessage = entity ? $filter("stringFormat")($filter("translate")("DeleteRoleMessage"), entity.Name) : "";
    }
})();
