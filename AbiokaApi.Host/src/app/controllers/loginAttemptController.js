(function () {
    'use strict';

    angular.module('abioka')
      .controller('LoginAttemptController', LoginAttemptController);

    /* @ngInject */
    function LoginAttemptController($timeout, $filter, AdminResource) {
        var vm = this;

        var fulldateFormat = $filter("translate")("FullDateFormat");
        var resultTemplate = "<span class='ab-label' ng-class=\"{'label-warning': entity.LoginResult === 'WrongPassword', 'label-success': entity.LoginResult === 'Successful'}\">{{entity.LoginResult | translate}}</span>"; 

        vm.options = {
            loadData: true,
            rowSelection: false,
            isReadOnly: true,
            resource: AdminResource.loginAttempts,
            query: {},
            columns: [{ name: "Date", text: "Date", order: true, cellTemplate: "<span>{{entity.Date | date:'" + fulldateFormat + "'}}</span>" },
                { name: "User.Email", text: "Email" },
            { name: "LoginResult", text: "LoginResult", cellTemplate: resultTemplate },
            { name: "IP", text: "IP" }],
        };
    }
})();
