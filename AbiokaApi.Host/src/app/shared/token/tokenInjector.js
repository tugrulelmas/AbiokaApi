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
            if (config.url.substring(0, 2) === "./") {
                config.url = "/api" + config.url.substring(1, config.url.length);
                var user = userService.getUser();
                if (user && user.IsSignedIn === true) {
                    config.headers.Authorization = "Bearer " + user.Token;
                }
            }
            return config;
        }
    }
})();