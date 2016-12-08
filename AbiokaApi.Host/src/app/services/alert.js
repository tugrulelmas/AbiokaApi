(function () {
    'use strict';

    angular.module('abioka')
      .service('alert', alert);

    /* @ngInject */
    function alert($mdToast) {

        var service = {
            success: success,
            info: info,
            warning: warning,
            error: error
        };
        return service;
        /*
        toastr.options.positionClass = 'toast-top-right';
        toastr.options.extendedTimeOut = 0; //1000;
        toastr.options.timeOut = 1000;
        toastr.options.fadeOut = 250;
        toastr.options.fadeIn = 250;
        */

        function success(message) {
            showMessage("success", message);
        }

        function info(message) {
            showMessage("info", message);
        }

        function warning(message) {
            showMessage("warning", message);
        }

        function error(message) {
            showMessage("error", message);
        }

        function showMessage(type, message) {
            //toastr[type](message);
            $mdToast.show($mdToast.simple()
                .position('top right')
                .textContent(message)
                .theme(type + '-toast'));
        }
    }
})();