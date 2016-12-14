(function () {
    'use strict';

    angular.module('abioka')
      .controller('MainController', MainController);

    /* @ngInject */
    function MainController($scope, $state, $mdSidenav, userService, translationService) {
        var vm = this;
        vm.user = userService.getUser();
        vm.logout = logout;
        vm.changeLanguage = changeLanguage;
        vm.openLeftMenu = openLeftMenu;
        vm.toggleMenu = toggleMenu;
        vm.menuItems = [{ "url": "#/", "text": "Dashboard" }, 
            { "url": "#/", "text": "Admin", children: [{ "url": "#/", "text": "Users" }, { "url": "#/", "text": "Roles" }] },
            { "url": "#/", "text": "Profile" },
        ];

        function changeLanguage(language) {
            var oldLanguage = userService.getUser().Language;
            if (oldLanguage !== language) {
                vm.user.Language = language;
                userService.updateUser(vm.user);
                translationService.setGlobalResources().then(function () {
                    //alert.info(vm.ml("LanguageChangedMessage")); 
                });
            }
        }

        function logout() {
            userService.destroy();
            vm.user = userService.getUser();
            $state.go("login");
        }

        function openLeftMenu() {
            $mdSidenav('left').toggle();
        };

        function toggleMenu(menuItem) {
            menuItem.isSelected = !menuItem.isSelected;
        };

        $scope.$on("bodyClassEvent", function (event, data) {
            vm.bodyClass = data;
        });
    }
})();
