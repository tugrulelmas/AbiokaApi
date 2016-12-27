(function () {
    'use strict';

    angular.module('abioka')
        .directive('ncgRequestVerificationToken', ncgRequestVerificationToken);

    /* @ngInject */
    function ncgRequestVerificationToken($http) {
        return function (scope, element, attrs) {
            $http.defaults.headers.common.RequestVerificationToken = attrs.ncgRequestVerificationToken || "no request verification token";
        };
    }
})();
