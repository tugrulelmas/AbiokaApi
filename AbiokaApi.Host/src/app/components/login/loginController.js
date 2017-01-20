(function () {
    'use strict';

    var login = {
        templateUrl: '/app/components/login/login.html',
        controller: LoginController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function LoginController($http, $state, userService, $auth, $rootScope, translationService) {
        var vm = this;
        vm.user = {};
        vm.login = login;
        vm.loading = false;
        vm.authenticate = authenticate;

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
            userService.setRememberMe(vm.rememberMe);
            internalLogin($http.post("./Auth/Login", angular.extend(vm.user, {
                provider: 'Local'
            })));
        }

        function authenticate(provider) {
            userService.setRememberMe(true);
            internalLogin($auth.authenticate(provider, { "provider": provider }));
        }

        function internalLogin(promise) {
            vm.loading = true;
            promise.then(function (response) {
                userService.setUser(response.data, function (user) {
                    $rootScope.$broadcast('userSignedIn', null);
                    translationService.setGlobalResources().then(function () {
                        $state.go("/");
                    });
                    vm.loading = false;
                });
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
