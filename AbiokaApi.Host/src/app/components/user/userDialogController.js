(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserDialogController', UserDialogController);

    /* @ngInject */
    function UserDialogController($filter, AdminResource, entity) {
        var vm = this;
        vm.resource = AdminResource.users;
        vm.entity = entity;
        vm.isUpdate = vm.entity && vm.entity.Id;
        vm.title = $filter("translate")("UserDetail");
        vm.deleteTitle = $filter("translate")("DeleteUser");
        vm.deleteMessage = vm.entity ? $filter("stringFormat")($filter("translate")("DeleteUserMessage"), vm.entity.Email) : "";
        vm.roles = [];

        activate();

        function activate() {
            AdminResource.roles.query({}, function (data) {
                vm.roles = data;
            });
        }

        /*
        $element.find('roleSearch').on('keydown', function (ev) {
            ev.stopPropagation();
        });
        */
    }
})();
