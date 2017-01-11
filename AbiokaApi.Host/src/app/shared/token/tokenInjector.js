(function () {
    'use strict';

    angular.module('abioka')
      .factory('tokenInjector', tokenInjector);

    /* @ngInject */
    function tokenInjector(userService) {
        var service = {
            request: request
        };
        return service;

        function request(config) {
            if (config.url.substring(0, 2) !== "./") {
                return config;
            }

            config.url = "/api" + config.url.substring(1, config.url.length);

            var userInfo = userService.getUser();
            if (!userInfo || !userInfo.IsSignedIn)
                return config;

            config.headers.Authorization = "Bearer " + userInfo.Token;
            return config;
        }
    }
})();