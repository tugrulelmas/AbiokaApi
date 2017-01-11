(function () {
    'use strict';

    angular.module('abioka')
      .factory('errorInjector', errorInjector);

    /* @ngInject */
    function errorInjector($rootScope, $q, $injector, $filter, userService) {
        var service = {
            responseError: responseError
        };
        return service;

        function responseError(rejection) {
            if (rejection.status === 401) {
                userService.destroy();
                $rootScope.$broadcast('userSignedOut', null);
                return $q.reject(rejection);
            }

            var alert = $injector.get("alert");
            var message = "";
            var statusReason = rejection.headers("Status-Reason");
            if (statusReason === "validation-failed") {
                var text = $filter("translate")(rejection.data.ErrorCode);
                if (rejection.data.Parameters && rejection.data.Parameters.length > 0) {
                    text = $filter("stringFormat")(text, rejection.data.Parameters);
                }
                if (rejection.status.toString().indexOf("40") === 0) {
                    alert.warning(text);
                } else {
                    message = text;
                }
            } else if (rejection.data && rejection.data.Message) {
                message = rejection.data.Message;
            } else if (rejection.data) {
                message = rejection.data;
            } else {
                message = angular.toJson(rejection);
            }

            if (message !== "") {
                alert.error(message);
            }
            $rootScope.$broadcast('errorOccurred', null);

            return $q.reject(rejection);
        }
    }
})();