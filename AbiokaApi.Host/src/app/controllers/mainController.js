(function () {
    'use strict';

    angular.module('abioka')
      .controller('MainController', MainController);

    /* @ngInject */
    function MainController($scope, $state, userService, translationService) {
        var vm = this;
        vm.user = userService.getUser();
        vm.logout = logout;
        vm.changeLanguage = changeLanguage;

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

        $scope.$on("bodyClassEvent", function (event, data) {
            vm.bodyClass = data;
        });
    }
})();
