(function () {
    'use strict';

    angular.module('abioka')
      .directive('backImg', backImg);

    /* @ngInject */
    function backImg(settingsService) {
        return {
            restrict: "E",
            link: function (scope, elem, attrs) {
                scope.$watch(function () {
                    return settingsService.getBackImg();
                }, function (newVal) {
                    var url = "none";
                    if (newVal && newVal !== "none") {
                        url = 'url(' + newVal + ')';
                    }
                    elem.css({
                        'background-image': url
                    });
                });
            }
        };
    }
})();