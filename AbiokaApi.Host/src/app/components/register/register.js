(function () {
    'use strict';

    var register = {
        templateUrl: '/app/components/register/register.html',
        controller: RegisterController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function RegisterController($http, userService, $state) {
        var vm = this;
        vm.model = {};
        vm.create = create;
        vm.authenticate = authenticate;
        vm.registered = false;
        vm.loading = false;

        activate();

        function activate() {
            if (userService.getUser().IsSignedIn) {
                $state.go("/");
            }
        }

        function create() {
            vm.loading = true;
            vm.model.Language = userService.getUser().Language;
            $http.post("./user/register", vm.model).then(function (response) {
                vm.registered = true;
            }, function () {
                vm.loading = false;
            });
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
      .component('register', register)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('register', {
                url: '/register',
                template: '<register></register>',
                isPublic: true
            });
    }
})();
