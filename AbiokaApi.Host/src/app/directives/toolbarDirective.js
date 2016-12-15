(function () {
    'use strict';

    angular.module('abioka')
      .directive('toolbar', toolbar);

    function toolbar() {
        var directive = {
            restrict: 'E',
            templateUrl: '/templates/shared/toolbar.html',
            replace: true
        };
        return directive;
    }
})();