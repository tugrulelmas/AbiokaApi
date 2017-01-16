(function () {
    'use strict';

    angular.module('abioka', [
        'ngMessages',
        'ngCookies',
        'ngResource',
        'ui.router',
        'ngMaterial',
        'md.data.table',
        'angularMoment']
     )
     .run(run)
     .config(config);

    /* @ngInject */
    function run($rootScope, $state, $stateParams, userService, translationService) {
        translationService.setGlobalResources();

        $rootScope.$on('$stateChangeStart', function (e, toState, toParams, fromState, fromParams) {
            if (toState.isPublic === true) {
                $rootScope.$broadcast('bodyClassEvent', 'login-page');
                return;
            }

            $rootScope.$broadcast('bodyClassEvent', '');
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
