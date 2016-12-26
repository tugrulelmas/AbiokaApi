(function () {
    'use strict';

    var validationMessage = {
        templateUrl: '/app/shared/validationMessage/validationMessage.html',
        bindings: {
            fieldName: '@',
            model: '<'
        }
    };

    angular.module('abioka')
      .component('validationMessage', validationMessage);
})();