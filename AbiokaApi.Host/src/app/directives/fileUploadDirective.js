(function () {
    'use strict';

    angular.module('abioka')
      .directive('fileUpload', fileUpload);

    function fileUpload() {
        var directive = {
            restrict: 'E',
            scope: {
                id: '@',
                uploadEvent: '&'
            },
            templateUrl: '/templates/shared/fileUpload.html',
            controller: fileUploadController,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;

        function fileUploadController() {
            var vm = this;
            vm.browse = browse;
            vm.fileSelected = fileSelected;
            vm.upload = upload;

            function browse() {
                document.getElementById(vm.id + 'FileUploadInput').click();
            }

            function fileSelected(params) {
                vm.selectedFile = params.event.target.files[0];
            }

            function upload() {
                vm.uploadEvent({ file: vm.selectedFile });
            }
        }
    }
})();
