(function () {
    'use strict';

    angular.module('abioka')
      .directive('editDialog', editDialog);

    function editDialog() {
        var directive = {
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: {
                loading: '=',
                title: '@',
                cancel: '&',
                save: '&'
            },
            templateUrl: '/templates/shared/editDialog.html',
            controller: editDialogController,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;

        function editDialogController() {
            var vm = this;
        }
    }
})();
