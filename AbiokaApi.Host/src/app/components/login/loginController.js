(function () {
    'use strict';

    var login = {
        templateUrl: '/app/components/login/login.html',
        controller: LoginController,
        controllerAs: 'vm'
    };

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
