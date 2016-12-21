(function () {
    'use strict';

    angular.module('abioka')
      .controller('LoginAttemptController', LoginAttemptController);

    /* @ngInject */
    function LoginAttemptController($timeout, AdminResource) {
        var vm = this;

        vm.options = {
            loadData: true,
            rowSelection: false,
            isReadOnly: true,
            resource: AdminResource.loginAttempts,
            query: {},
            columns: [{ name: "Date", text: "Date", order: true },
                { name: "User.Email", text: "Email" },
            { name: "LoginResult", text: "LoginResult", filter: "translate"},
            { name: "IP", text: "IP" }],
        };
    }
})();
