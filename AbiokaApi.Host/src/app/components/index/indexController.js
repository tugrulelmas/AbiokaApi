(function () {
    'use strict';

    var index = {
        templateUrl: '/app/components/index/index.html',
        controller: IndexController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function IndexController() {
        var vm = this;
    }

    angular.module('abioka')
      .component('index', index)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('/', {
                url: '/',
                template: '<index></index>',
                isPublic: false
            });
    }
})();
