(function () {
    'use strict';

    var loginAttempts = {
        templateUrl: '/app/components/loginAttempt/loginAttempts.html',
        controller: LoginAttemptsController,
        controllerAs: 'vm'
    };

    /* @ngInject */
    function LoginAttemptsController($filter, AdminResource) {
        var vm = this;

        var resultTemplate = "<span class='ab-label' ng-class=\"{'label-warning': entity.LoginResult === 'WrongPassword', 'label-success': entity.LoginResult === 'Successful'}\">{{entity.LoginResult | translate}}</span>";
        var fulldateFormat = $filter("translate")("FullDateFormat");
        var dateTemplate = "<span>{{entity.Date | abDate:'" + fulldateFormat + "'}}</span>";

        vm.options = {
            loadOnInit: true,
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

    angular.module('abioka')
      .component('loginAttempts', loginAttempts)
      .config(config);

    /* @ngInject */
    function config($stateProvider) {
        $stateProvider
            .state('loginAttempts', {
                url: '/loginAttempts',
                template: '<login-attempts></login-attempts>',
                isPublic: false
            });
    }
})();
