(function () {
    'use strict';

    angular.module('abioka')
      .service('settingsService', settingsService);

    /* @ngInject */
    function settingsService() {
        var backImg = "";

        var service = {
            setBackImg: setBackImg,
            getBackImg: getBackImg
        };

        return service;

        function setBackImg(url) {
            backImg = url;
        }

        function getBackImg() {
            return backImg;
        }
    }
})();