(function () {
    'use strict';

    angular.module('abioka')
      .directive('menu', menu);

    function menu() {
        var directive = {
            restrict: 'E',
            templateUrl: '/app/shared/menu/menu.html',
            replace: true
        };
        return directive;
    }
})();