(function () {
    'use strict';

    angular.module('abioka')
      .controller('MenuDialogController', MenuDialogController);

    /* @ngInject */
    function MenuDialogController($filter, $q, $http, AdminResource, entity) {
        var vm = this;
        vm.resource = AdminResource.menus;
        vm.entity = entity;
        vm.title = $filter("translate")("MenuDetail");
        vm.deleteTitle = $filter("translate")("DeleteMenu");
        vm.deleteMessage = entity ? $filter("stringFormat")($filter("translate")("DeleteMenuMessage"), entity.Text) : "";
        vm.searchParent = searchParent;

        activate();

        function activate() {
            AdminResource.roles.query({}, function (data) {
                vm.roles = data;
            });
        }

        function searchParent(text) {
            var deferred = $q.defer();
            $http.get("./menu/filter?text=" + text).then(function (result) {
                deferred.resolve(result.data);
            });
            return deferred.promise;
        }
    }
})();
