(function () {
    'use strict';

    var changePassword = {
        templateUrl: '/app/components/changePassword/changePassword.html',
        controller: ChangePasswordController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function ChangePasswordController($http, userService, alert) {
        var vm = this;
        vm.user = userService.getUser();
        vm.model = { "UserId": vm.user.Id };
        vm.changePassword = changePassword;

        function changePassword() {
            $http.put("./user/" + vm.user.Id + '/changePassword', vm.model).then(function (response) {
                vm.modelForm.$setPristine();
                vm.modelForm.$setUntouched();
                vm.model = { "UserId": vm.user.Id };

                alert.success("PasswordChanged", true);
                userService.setUser(response.data);
            });
        }
    }

    angular.module('abioka')
      .component('changePassword', changePassword)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('changePassword', {
                url: '/changePassword',
                template: '<change-password></change-password>',
                isPublic: false
            });
    }
})();
