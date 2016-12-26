(function () {
    'use strict';

    angular.module('abioka')
      .controller('LoginController', LoginController);

    /* @ngInject */
    function LoginController($scope, localSignInService) {
        var vm = this;
        vm.user = {};
        vm.login = login;

        function login() {
            vm.loading = true;
            localSignInService.login(vm.user).then(function () {
                vm.loading = false;
            }, function (reason) {
                vm.loading = false;
            });
        }
    }
})();
