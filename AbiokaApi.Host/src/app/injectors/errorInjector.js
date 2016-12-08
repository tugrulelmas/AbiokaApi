(function () {
    'use strict';

    angular.module('abioka')
      .factory('errorInjector', errorInjector);

    /* @ngInject */
    function errorInjector($rootScope, $q, $injector) {
        var service = {
            responseError: responseError
        }
        return service;

        function responseError(rejection) {
            var alert = $injector.get("alert");
            if (rejection.status === 401) {
                userService.destroy();
                $rootScope.$broadcast('userSignedOut', null);
                return $q.reject(rejection);
            }

            var message = "";
            var statusReason = rejection.headers("Status-Reason");
            if (statusReason === "validation-failed") {
                message = rejection.data;
            } else if (rejection.data && rejection.data.Message) {
                message = rejection.data.Message;
            } else if (rejection.data) {
                message = rejection.data;
            } else {
                message = angular.toJson(rejection);
            }
            alert.error(message);

            return $q.reject(rejection);
        }
    }
})();