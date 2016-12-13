(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserDialogController', UserDialogController);

    /* @ngInject */
    function UserDialogController($mdDialog, $filter, UserResource, user) {
        var vm = this;
        vm.resource = UserResource.users;
        vm.entity = user;
        vm.isUpdate = user && user.Id;
        vm.title = $filter("translate")("UserDetail");
        vm.deleteTitle = $filter("translate")("DeleteUser");
        vm.deleteMessage = user ? $filter("stringFormat")($filter("translate")("DeleteUserMessage"), user.Email) : "";
    }
})();
