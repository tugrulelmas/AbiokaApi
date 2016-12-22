(function () {
    'use strict';

    angular.module('abioka')
      .controller('LoginAttemptController', LoginAttemptController);

    /* @ngInject */
    function LoginAttemptController($timeout, $filter, AdminResource) {
        var vm = this;

        var resultTemplate = "<span class='ab-label' ng-class=\"{'label-warning': entity.LoginResult === 'WrongPassword', 'label-success': entity.LoginResult === 'Successful'}\">{{entity.LoginResult | translate}}</span>";
        var fulldateFormat = $filter("translate")("FullDateFormat");
        var dateTemplate = "<span>{{entity.Date | abDate:'" + fulldateFormat + "'}}</span>";

        vm.options = {
            loadData: true,
            rowSelection: false,
            isReadOnly: true,
            resource: AdminResource.loginAttempts,
            query: { order: '-Date' },
            columns: [{ name: "Date", text: "Date", order: true, cellTemplate: dateTemplate },
                { name: "User.Email", text: "Email" },
            { name: "LoginResult", text: "LoginResult", cellTemplate: resultTemplate },
            { name: "IP", text: "IP" }],
        };
    }
})();
