(function () {
    'use strict';

    angular.module('abioka')
      .controller('IndexController', IndexController);

    /* @ngInject */
    function IndexController($timeout) {
        var vm = this;

        vm.options = {};
        vm.entities = { count: 2, data: [{ "Id": 1, "Name": "Foo 1" }, { "Id": 2, "Name": "Foo 2" }] };
        vm.selected = [];
        vm.getData = getData;
        vm.promise = $timeout(function () { }, 2000);
        vm.query = {
            order: 'Name',
            limit: 5,
            page: 1
        };

        activate();

        function activate() {
            vm.options.rowSelection = true;
        }

        function getData() {
            //vm.promise = FooService.foos.get(vm.query, success).$promise;
            vm.promise = $q(function(resolve, reject) {
              $timeout(function() {
                  vm.entities = {count: 2, data: [{"Id": 3, "Name": "Foo 3"}, {"Id": 4, "Name": "Foo 4"}]};
                  resolve();
              }, 5000);
            });
        }

        function success(data) {
            vm.entities = data;
        }
    }
})();
