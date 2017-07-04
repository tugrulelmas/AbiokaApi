(function () {
    'use strict';

    var register = {
        templateUrl: '/app/components/verify/verify.html',
        controller: VerifyController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function VerifyController($http, $stateParams, $state) {
        var vm = this;
        vm.loading = false;
        var id = $stateParams.id;

        activate();

        function activate() {
            vm.loading = true;
            $http.put("./user/" + id + "/verify").then(function (response) {
                $state.go("login");
            }, function () {
                vm.loading = false;
            });
        }
    }

    angular.module('abioka')
      .component('verify', register)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('verify', {
                url: '/verify/:id',
                template: '<verify></verify>',
                isPublic: true
            });
    }
})();
