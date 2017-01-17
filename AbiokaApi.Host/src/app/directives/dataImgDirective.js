(function () {
    'use strict';

    angular.module('abioka')
      .directive('abDataImg', abDataImg);

    /* @ngInject */
    function abDataImg(settingsService) {
        return {
            restrict: "A",
            link: function (scope, elem, attrs) {
                scope.$watch(function () {
                    return settingsService.getBackImg();
                }, function (newVal) {
                    var result = "false";
                    if (newVal && newVal !== "none") {
                        result = "true";
                    }
                    elem.attr('data-image', result);
                });
            }
        };
    }
})();