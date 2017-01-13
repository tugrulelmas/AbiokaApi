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
        vm.loading = false;

        activate();

        function activate() {
            if (userService.getUser().IsSignedIn) {
                $state.go("/");
            }
        }

        function create() {
            vm.loading = true;
            $http.post("./user/register", vm.model).then(function (response) {
                $state.go("login");
            }, function () {
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
