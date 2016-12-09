(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserDialogController', UserDialogController);

    /* @ngInject */
    function UserDialogController($mdDialog, UserResource, user) {
        var vm = this;
        vm.user = user;
        vm.cancel = cancel;
        vm.dialog = false;
        vm.isUpdate = user !== null && user.Id !== null;
        vm.title = "User Detail";
        vm.deleteMessage = user !== null ? "Do you really want to delete user whose email is " + user.Email : "";
        vm.save = save;
        vm.deleteEntity = deleteEntity;

        function cancel() {
            $mdDialog.cancel();
        }

        function save() {
            vm.loading = true;
            if (vm.isUpdate) {
                UserResource.users.update({}, vm.user).$promise.then(closeDialog);
            } else {

            }
        }

        function deleteEntity() {
            vm.loading = true;
            UserResource.users.delete({ id: vm.user.Id }).$promise.then(closeDialog);
        }

        function closeDialog() {
            vm.dialog = false;
            $mdDialog.hide(vm.user);
        }
    }
})();
