(function () {
    'use strict';

    angular.module('abioka', [
        'ngMessages',
        'ngCookies',
        'ngResource',
        'ui.router',
        'ngMaterial',
        'md.data.table',
        'angularMoment',
        'satellizer',
        'ngMdIcons']
     )
     .run(run)
     .config(config);

    /* @ngInject */
    function run($rootScope, $state, $stateParams, userService, translationService, settingsService) {
        translationService.setGlobalResources();

        $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
            if (toState.isPublic === true) {
                //settingsService.setBackImg("../assets/images/login.jpg");
                settingsService.setBackImg("https://source.unsplash.com/random");
                return;
            }

            settingsService.setBackImg("none");

            var user = userService.getUser();
            if (!user.IsSignedIn) {
                e.preventDefault();
                $state.transitionTo("login", null, {
                    notify: false
                });
                $state.go("login");
            }
        });
    }

    /* @ngInject */
    function config($urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

        $urlRouterProvider.otherwise('/');
    }
})();
