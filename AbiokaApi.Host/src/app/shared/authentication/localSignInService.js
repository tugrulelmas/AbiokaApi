(function () {
    'use strict';

    angular.module('abioka')
      .service('localSignInService', localSignInService);

    /* @ngInject */
    function localSignInService($rootScope, $q, $http, $state, userService) {
        var service = {
            login: login,
            logout: logout
        };
        return service;

        function login(localUser) {
            var deferred = $q.defer();
            $http.post("./User/Login", localUser).success(function (data) {
                userService.setUser(data, function (user) {
                    $state.go("/");
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
