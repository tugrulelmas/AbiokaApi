(function () {
    'use strict';

    angular.module('abioka')
      .controller('MainController', MainController);

    /* @ngInject */
    function MainController($scope, $state, userService) {
        var vm = this;
        vm.user = userService.getUser();
        vm.logout = logout;

        $scope.$on("bodyClassEvent", function (event, data) {
            vm.bodyClass = data;
        });

        function logout() {
            userService.destroy();
            vm.user = userService.getUser();
            $state.go("login");
        }
    }
})();
