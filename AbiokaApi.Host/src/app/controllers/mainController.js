(function () {
    'use strict';

    angular.module('abioka')
      .controller('MainController', MainController);

    /* @ngInject */
    function MainController($scope, userService) {
        var vm = this;
        vm.user = userService.getUser();
        vm.toggleMenu = toggleMenu;
        vm.menuItems = [{ "url": "/", "text": "Dashboard" }, 
            { "url": "/", "text": "Admin", children: [{ "url": "users", "text": "Users" }, { "url": "roles", "text": "Roles" }] },
            { "url": "loginAttempts", "text": "LoginLogs" },
        ];

        function toggleMenu(menuItem) {
            menuItem.isSelected = !menuItem.isSelected;
        };

        $scope.$on("userSignedOut", function (event, data) {
            vm.user = userService.getUser();
        });
    }
})();
