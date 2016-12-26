(function () {
    'use strict';

    angular.module('abioka')
      .filter('picker', picker);

    function picker($filter) {
        var filter = function (value, filterName) {
            if (!filterName) {
                return value;
            }
            return $filter(filterName)(value);
        };

        filter.$stateful = true;
        return filter;
    }
})();