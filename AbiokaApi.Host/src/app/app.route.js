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
            }).state('users', {
                url: '/users',
                templateUrl: 'templates/admin/users.html',
                controller: 'UserController',
                controllerAs: 'vm',
                isPublic: false
            }).state('roles', {
                url: '/roles',
                templateUrl: 'templates/admin/roles.html',
                controller: 'RoleController',
                controllerAs: 'vm',
                isPublic: false
            });
    }
})();
