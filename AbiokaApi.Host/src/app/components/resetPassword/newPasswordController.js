(function () {
    'use strict';

    var newPassword = {
        templateUrl: '/app/components/resetPassword/newPassword.html',
        controller: NewPasswordController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function NewPasswordController($http, $stateParams, $state) {
        var vm = this;
        vm.model = {};
        vm.resend = resend;
        vm.loading = false;
        var id = $stateParams.id;

        function resend() {
            vm.loading = true;
            $http.post("./user/" + id + "/NewPassword", { Token: id, Password: vm.model.NewPassword }).then(function (response) {
                $state.go("login");
            }, function () {
                vm.loading = false;
            });
        }
    }

    angular.module('abioka')
      .component('newPassword', newPassword)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('newPassword', {
                url: '/newPassword/:id',
                template: '<new-password></new-password>',
                isPublic: true
            });
    }
})();
