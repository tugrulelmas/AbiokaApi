(function () {
    'use strict';

    var login = {
        templateUrl: '/app/components/login/login.html',
        controller: LoginController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function LoginController($http, $state, localSignInService, userService) {
        var vm = this;
        vm.user = {};
        vm.login = login;
        vm.loading = false;

        activate();

        function activate() {
            if (userService.getUser().IsSignedIn) {
                $state.go("/");
            } else {
                $http.get("./installation/required").then(function (response) {
                    if (response.data === true) {
                        $state.go("install");
                    }
                });
            }
        }

        function login() {
            vm.loading = true;
            userService.setRememberMe(vm.rememberMe);
            localSignInService.login(vm.user).then(function () {
                vm.loading = false;
            }, function (reason) {
                vm.loading = false;
            });
        }
    }

    angular.module('abioka')
      .component('login', login)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('login', {
                url: '/login',
                template: '<login></login>',
                isPublic: true
            });
    }
})();
