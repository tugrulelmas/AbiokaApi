(function () {
    'use strict';

    angular.module('abioka')
	  .config(config);

    /* @ngInject */
    function config($httpProvider, $mdThemingProvider) {
        $httpProvider.interceptors.push('tokenInjector');
        $httpProvider.interceptors.push('errorInjector');

        $mdThemingProvider
            .theme('default')
            .primaryPalette('blue');
    }
})();