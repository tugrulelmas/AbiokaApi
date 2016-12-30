(function () {
    'use strict';

    var install = {
        templateUrl: '/app/components/install/install.html',
        controller: InstallController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function InstallController($http, $state) {
        var vm = this;
        vm.entity = {};
        vm.create = create;

        activate();

        function activate() {
            $http.get("./installation/required").then(function (response) {
                if (!response.data) {
                    $state.go("login");
                }
            });
        }

        function create() {
            $http.post("./installation/create", vm.entity).then(function (response) {
                $state.go("login");
            });
        }
    }

    angular.module('abioka')
      .component('install', install)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('install', {
                url: '/install',
                template: '<install></install>',
                isPublic: true
            });
    }
})();
