(function () {
    'use strict';

    angular.module('abioka')
      .directive('validationMessage', validationMessage);

    function validationMessage() {
        var directive = {
            restrict: 'E',
            templateUrl: '/templates/shared/messages.html',
            scope: {
                fieldName: '@',
                model: '='
            },
            replace: false
        };
        return directive;
    }
})();