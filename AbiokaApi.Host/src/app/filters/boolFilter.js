(function () {
    'use strict';

    angular.module('abioka')
      .filter('boolFilter', boolFilter);

    /* @ngInject */
    function boolFilter($filter) {
        return function (value) {
            var text = value === true ? "True" : "False";
            return $filter("translate")(text);
        };
    }
})();