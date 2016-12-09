(function () {
    'use strict';

    angular.module('abioka')
      .controller('IndexController', IndexController);

    /* @ngInject */
    function IndexController($timeout, $mdDialog, UserResource) {
        var vm = this;

        vm.options = {};
        vm.showDialog = showDialog;
        vm.showDeleteDialog = showDeleteDialog;
        vm.query = { order: 'Email' };
        vm.resource = UserResource.users;

        activate();

        function activate() {
            vm.options.rowSelection = false;
        }

        function showDialog(event, entity) {
            return showEditOrDeleteDialog(event, entity, '/templates/userDialog.html', false);
        }

        function showDeleteDialog(event, entity) {
            return showEditOrDeleteDialog(event, entity, '/templates/shared/deleteComponent.html', true);
        }

        function showEditOrDeleteDialog(ev, user, template, isDelete) {
            return $mdDialog.show({
                controller: 'UserDialogController',
                controllerAs: 'vm',
                templateUrl: template,
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                locals: {
                    user: user
                }
            });
        }
    }
})();
