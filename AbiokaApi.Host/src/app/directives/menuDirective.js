(function () {
    'use strict';

    angular.module('abioka')
      .directive('menu', menu);

    function menu() {
        var directive = {
            restrict: 'E',
            templateUrl: '/templates/shared/menu.html',
            replace: true
        };
        return directive;
    }
})();