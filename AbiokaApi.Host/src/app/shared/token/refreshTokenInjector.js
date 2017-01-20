(function () {
    'use strict';

    angular.module('abioka')
      .factory('refreshTokenInjector', refreshTokenInjector);

    /* @ngInject */
    function refreshTokenInjector(userService, $injector, $q) {
        var service = {
            request: request
        };
        return service;

        function request(config) {
            var deferred = $q.defer();

            if (config.url.substring(0, 2) !== "./") {
                deferred.resolve(config);
                return deferred.promise;
            }

            var userInfo = userService.getUser();
            if (!userInfo || !userInfo.IsSignedIn) {
                deferred.resolve(config);
                return deferred.promise;
            }

            var now = parseInt(new Date().getTime() / 1000);
            if (userInfo.ExpirationDate > now) {
                deferred.resolve(config);
                return deferred.promise;
            }

            var $http = $injector.get("$http");
            $http.post("/api/auth/refreshToken?refreshToken=" + userInfo.RefreshToken + "&provider=" + userInfo.Provider).then(function (response) {
                userService.setUser(response.data, function (userData) {
                    deferred.resolve(config);
                });
            }, function () {
                deferred.resolve(config);
            });

            return deferred.promise;
        }
    }
})();