(function () {
    'use strict';

    var toolbar = {
        templateUrl: '/templates/shared/toolbar.html',
        controller: controller
    };

    /* @ngInject */
    function controller($scope, $state, userService, translationService, $mdSidenav) {
        var self = this;
        self.changeLanguage = changeLanguage;
        self.logout = logout;
        self.openLeftMenu = openLeftMenu;
        self.user = userService.getUser();

        function changeLanguage(language) {
            var oldLanguage = userService.getUser().Language;
            if (oldLanguage !== language) {
                self.user.Language = language;
                userService.updateUser(self.user);
                translationService.setGlobalResources().then(function () {
                    //alert.info(vm.ml("LanguageChangedMessage")); 
                });
            }
        }

        function logout() {
            userService.destroy();
            self.user = userService.getUser();
            $scope.$emit('userSignedOut', null);
            $state.go("login");
        }

        function openLeftMenu() {
            $mdSidenav('left').toggle();
        };
    }

    angular.module('abioka')
      .component('toolbar', toolbar);
})();