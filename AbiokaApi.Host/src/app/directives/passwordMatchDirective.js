(function () {
    'use strict';

    angular.module('abioka')
      .directive('passwordMatch', passwordMatch);

    function passwordMatch() {
        var directive = {
            require: 'ngModel',
            scope: {
                otherModelValue: '=passwordMatch'
            },
            link: link
        };
        return directive;

        /* @ngInject */
        function link(scope, element, attributes, ngModel) {
            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue === scope.otherModelValue;
            };
            scope.$watch('otherModelValue', function () {
                ngModel.$validate();
            });
        }
    }
})();