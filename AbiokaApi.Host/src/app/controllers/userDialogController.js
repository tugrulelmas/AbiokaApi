(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserDialogController', UserDialogController);

    /* @ngInject */
    function UserDialogController($mdDialog, UserResource, user) {
        var vm = this;
        vm.resource = UserResource.users;
        vm.entity = user;
        vm.isUpdate = user && user.Id;
        vm.title = "User Detail";
        vm.deleteTitle = "Delete User";
        vm.deleteMessage = user ? "Do you really want to delete user whose email is " + user.Email : "";
    }
})();
