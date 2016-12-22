(function () {
    'use strict';

    angular.module('abioka')
      .filter('abDate', abDate);

    /* @ngInject */
    function abDate(moment) {
        return function (input, format) {
            if (angular.isDefined(format)) {
                return moment.utc(input).local().format(format);
            }
            else {
                return moment.utc(input).local();
            }
        };
    }
})();