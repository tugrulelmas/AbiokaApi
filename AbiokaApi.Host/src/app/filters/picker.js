(function () {
    'use strict';

    angular.module('abioka')
      .filter('picker', picker);

    function picker($filter) {
        return function (value, filterName) {
            if (!filterName) {
                return value;
            }
            return $filter(filterName)(value);
        }
    }
})();