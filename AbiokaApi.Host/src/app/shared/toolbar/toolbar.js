(function () {
    'use strict';

    var toolbar = {
        templateUrl: '/app/shared/toolbar/toolbar.html',
        controller: ToolbarController
    };

    /* @ngInject */
    function ToolbarController($scope, $state, userService, translationService, $mdSidenav, $http) {
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

                if (self.user.IsSignedIn) {
                    $http.put("./user/" + self.user.Id + "/ChangeLanguage?language=" + language).then(function () { });
                }
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
        }

        $scope.$on("userSignedOut", function (event, data) {
            self.user = userService.getUser();
            $state.go("login");
        });
    }

    angular.module('abioka')
      .component('toolbar', toolbar);
})();