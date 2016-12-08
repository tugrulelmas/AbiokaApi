(function () {
    'use strict';

    angular.module('abioka')
      .controller('WeddingController', WeddingController);

    /* @ngInject */
    function WeddingController($http, $stateParams, uiService) {
        var vm = this;

        var id = $stateParams.id;
        $http.get("./public/" + id).success(function (data) {
            vm.model = data;
            uiService.init();
        });
    }
})();
