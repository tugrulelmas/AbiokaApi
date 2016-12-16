(function () {
    'use strict';

    angular.module('abioka')
      .directive('classGtSm', classGtSm);

    /* @ngInject */
    function classGtSm($mdMedia) {
        if ($mdMedia("gt-sm")) {
            return {
                restrict: "A",
                link: function (scope, elem, attrs) {
                    elem.addClass(attrs.classGtSm);
                }
            };
        }
    }
})();