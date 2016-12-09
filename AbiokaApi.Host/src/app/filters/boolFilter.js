(function () {
    'use strict';

    angular.module('abioka')
      .filter('boolFilter', boolFilter);

    function boolFilter() {
        return function (value) {
            return value === true ? "True" : "False";
        }
    }
})();