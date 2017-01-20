(function () {
    'use strict';

    angular.module('abioka')
	  .config(config);

    /* @ngInject */
    function config($httpProvider, $mdThemingProvider, $authProvider) {
        $authProvider.google({
            clientId: '529698767409-guf49773k4neu58n35rbg77nj6e8r29l.apps.googleusercontent.com',
            url: '/api/auth/login',
            optionalUrlParams: ['display', 'access_type', 'approval_prompt'],
            accessType: 'offline', 
            approvalPrompt: 'force'
        });

        $authProvider.facebook({
            clientId: '1898771637065664',
            url: '/api/auth/login',
        });

        $httpProvider.interceptors.push('refreshTokenInjector');
        $httpProvider.interceptors.push('tokenInjector');
        $httpProvider.interceptors.push('errorInjector');

        $mdThemingProvider
            .theme('default')
            .primaryPalette('blue');
    }
})();