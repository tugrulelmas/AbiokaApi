(function () {
    'use strict';

    var validationMessage = {
        templateUrl: '/templates/shared/messages.html',
        bindings: {
            fieldName: '@',
            model: '<'
        }
    };

    angular.module('abioka')
      .component('validationMessage', validationMessage);
})();