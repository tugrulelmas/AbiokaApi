(function () {
    'use strict';

    var resetPassword = {
        templateUrl: '/app/components/resetPassword/resetPassword.html',
        controller: ResetPasswordController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function ResetPasswordController($http) {
        var vm = this;
        vm.model = {};
        vm.resend = resend;
        vm.resent = false;
        vm.loading = false;

        function resend() {
            vm.loading = true;
            $http.post("./user/" + vm.model.Email + "/ResetPassword").then(function (response) {
                vm.resent = true;
            }, function () {
                vm.loading = false;
            });
        }
    }

    angular.module('abioka')
      .component('resetPassword', resetPassword)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('resetPassword', {
                url: '/resetPassword',
                template: '<reset-password></reset-password>',
                isPublic: true
            });
    }
})();
