(function () {
    'use strict';

    angular.module('abioka')
      .controller('IndexController', IndexController);

    /* @ngInject */
    function IndexController($timeout, $mdDialog, UserResource) {
        var vm = this;

        vm.options = {};
        vm.entities = {};
        vm.selected = [];
        vm.getData = getData;
        vm.promise = $timeout(function () { }, 2000);
        vm.showDialog = showDialog;
        vm.showDeleteDialog = showDeleteDialog;
        vm.query = {
            order: 'Name',
            limit: 5,
            page: 1
        };

        activate();

        function activate() {
            vm.options.rowSelection = false;
            getData();
        }

        function getData() {
            vm.promise = UserResource.users.get(vm.query, success).$promise;
        }

        function success(data) {
            vm.entities = data;
        }

        function showDialog(ev, user) {
            showEditOrDeleteDialog(ev, user, '/templates/userDialog.html', false);
        }

        function showDeleteDialog(ev, user) {
            showEditOrDeleteDialog(ev, user, '/templates/shared/deleteDialog.html', true);
        }

        function showEditOrDeleteDialog(ev, user, template, isDelete) {
            var tmpUser = angular.copy(user);
            $mdDialog.show({
                controller: 'UserDialogController',
                controllerAs: 'vm',
                templateUrl:template ,
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: false,
                locals: {
                    user: tmpUser
                }
            })
            .then(function (updatedUser) {
                if (isDelete) {
                    vm.entities.Data.splice(vm.entities.Data.indexOf(user), 1);
                } else {
                    angular.copy(updatedUser, user);
                }
            }, function () {
            });
        }
    }
})();
