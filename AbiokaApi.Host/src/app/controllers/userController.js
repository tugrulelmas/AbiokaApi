(function () {
    'use strict';

    angular.module('abioka')
      .controller('UserController', UserController);

    /* @ngInject */
    function UserController($timeout, $mdDialog, AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            resource: AdminResource.users,
            query: {},
            columns: [{ name: "Email", text: "Email", order: true },
                      { name: "IsAdmin", text: "IsAdmin", filter: 'boolFilter' }]
        };
        vm.showDialog = showDialog;
        vm.showDeleteDialog = showDeleteDialog;

        function showDialog(event, entity) {
            return showEditOrDeleteDialog(event, entity, '/templates/admin/userDialog.html', false);
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
