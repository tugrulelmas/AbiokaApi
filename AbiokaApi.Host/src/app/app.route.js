(function () {
    'use strict';

    angular.module('abioka')
       .config(routeConfig);

    /* @ngInject */
    function routeConfig($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.html5Mode({
            enabled: false,
            requireBase: false
        });

        $urlRouterProvider.otherwise('/');

        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: '/templates/login.html',
                controller: 'LoginController',
                controllerAs: 'vm',
                isPublic: true
            }).state('/', {
                url: '/',
                templateUrl: 'templates/index.html',
                controller: 'IndexController',
                controllerAs: 'vm',
                isPublic: false
            });
    }
})();
