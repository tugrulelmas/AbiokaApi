(function () {
    'use strict';

    angular.module('abioka')
      .filter('translate', translate);

    /* @ngInject */
    function translate(translationService) {
        function localization(value) {
            return translationService.getResource(value);
        }

        localization.$stateful = true;
        return localization;
    }
})();