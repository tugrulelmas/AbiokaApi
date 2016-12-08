(function () {
    'use strict';

    angular.module('abioka')
        .directive('backImg', backImg);

    function backImg() {
        return function (scope, element, attrs) {
            scope.$watch(attrs.backImg, function (newVal) {
                if (newVal) {
                    element.css({
                        'background-image': 'url(' + newVal + ')'
                    });
                }
            });
        };
    }
})();
