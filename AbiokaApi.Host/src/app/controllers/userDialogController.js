(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserDialogController', UserDialogController);

    /* @ngInject */
    function UserDialogController($mdDialog, $filter, AdminResource, user) {
        var vm = this;
        vm.resource = AdminResource.users;
        vm.entity = user;
        vm.isUpdate = user && user.Id;
        vm.title = $filter("translate")("UserDetail");
        vm.deleteTitle = $filter("translate")("DeleteUser");
        vm.deleteMessage = user ? $filter("stringFormat")($filter("translate")("DeleteUserMessage"), user.Email) : "";
        vm.roles = [];
        vm.activate = activate;

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
