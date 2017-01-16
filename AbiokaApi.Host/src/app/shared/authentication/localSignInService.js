(function () {
    'use strict';

    angular.module('abioka')
      .service('localSignInService', localSignInService);

    /* @ngInject */
    function localSignInService($rootScope, $q, $http, $state, userService, translationService) {
        var service = {
            login: login,
            logout: logout
        };
        return service;

        function login(localUser) {
            var deferred = $q.defer();
            $http.post("./User/Login", localUser).then(function (response) {
                userService.setUser(response.data, function (user) {
                    translationService.setGlobalResources().then(function () {
                        $state.go("/");
                    });
                });
                deferred.resolve();
            }, function (response) {
                deferred.reject(response);
            });
            return deferred.promise;
        }

        function logout() {
            var deferred = $q.defer();
            deferred.resolve();
            return deferred.promise;
        }
    }
})();
