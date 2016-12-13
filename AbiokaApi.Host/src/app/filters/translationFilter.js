(function () {
    'use strict';

    angular.module('abioka')
      .filter('translate', translate);

    /* @ngInject */
    function translate(translationService) {
        return function (value) {
            return translationService.getResource(value);
        }
    }
})();