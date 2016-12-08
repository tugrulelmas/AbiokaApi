(function () {
    'use strict';

    angular.module('abioka')
      .directive('customOnChange', customOnChange);

    function customOnChange() {
        var directive = {
            restrict: 'A',
            scope: {
                handler: '&'
            },
            link: link
        };
        return directive;

        function link(scope, element, attrs) {
            element.change(function (event) {
                scope.$apply(function () {
                    var params = { event: event, el: element };
                    scope.handler({ params: params });
                });
            });
        }
    }
})();
