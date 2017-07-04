(function () {
    'use strict';

    var resendVerification = {
        templateUrl: '/app/components/resendVerification/resendVerification.html',
        controller: ResendVerificationController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function ResendVerificationController($http) {
        var vm = this;
        vm.model = {};
        vm.resend = resend;
        vm.resent = false;
        vm.loading = false;

        function resend() {
            vm.loading = true;
            $http.post("./user/" + vm.model.Email + "/ResendVerification").then(function (response) {
                vm.resent = true;
            }, function () {
                vm.loading = false;
            });
        }
    }

    angular.module('abioka')
      .component('resendVerification', resendVerification)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('resendVerification', {
                url: '/resendVerification',
                template: '<resend-verification></resend-verification>',
                isPublic: true
            });
    }
})();
