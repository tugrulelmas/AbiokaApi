(function () {
    'use strict';

    var toolbar = {
        templateUrl: '/templates/shared/toolbar.html',
        bindings: {
            logout: '&',
            isSignedIn: '<',
            changeLanguage: '&',
            openLeftMenu: '&'
        },
        controller: function () {
            var a = this.isSignedIn;
        }
    };

    angular.module('abioka')
      .component('toolbar', toolbar);
})();