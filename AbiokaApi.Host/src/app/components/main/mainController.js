(function () {
    'use strict';

    angular.module('abioka')
      .controller('MainController', MainController);

    /* @ngInject */
    function MainController($scope, $http, userService) {
        var vm = this;
        vm.user = {};
        vm.toggleMenu = toggleMenu;
        vm.menuItems = [];

        activate();

        function activate() {
            vm.user = userService.getUser();

            if (vm.user.IsSignedIn) {
                $http.get("./menu").then(function (response) {
                    vm.menuItems = response.data;
                });
            }
        }

        function toggleMenu(menuItem) {
            menuItem.isSelected = !menuItem.isSelected;
        }

        $scope.$on("userSignedOut", function (event, data) {
            vm.user = userService.getUser();
        });

        $scope.$on("userSignedIn", function (event, data) {
            activate();
        });
    }
})();
