(function () {
    'use strict';

    angular.module('abioka')
      .service('alert', alert);

    /* @ngInject */
    function alert($mdToast, $filter) {

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

        function success(message, translate) {
            showMessage("success", message, translate);
        }

        function info(message, translate) {
            showMessage("info", message, translate);
        }

        function warning(message, translate) {
            showMessage("warning", message, translate);
        }

        function error(message, translate) {
            showMessage("error", message, translate);
        }

        function showMessage(type, message, translate) {
            var text = message;
            if (translate === true) {
                text = $filter("translate")(message);
            }
            //toastr[type](message);
            $mdToast.show($mdToast.simple()
                .position('top right')
                .textContent(text)
                .toastClass(type + '-toast'));
        }
    }
})();