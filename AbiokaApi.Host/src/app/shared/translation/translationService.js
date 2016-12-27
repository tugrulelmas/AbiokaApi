(function () {
    'use strict';

    angular.module('abioka')
      .service('translationService', translationService);

    /* @ngInject */
    function translationService($resource, $q, userService) {
        var resources = [];
        var resourceLoaded = false;

        var service = {
            getResource: getResource,
            setGlobalResources: setGlobalResources
        };
        return service;

        function getRecourcesFromFileOrCache(languageFilePath) {
            var sessionData = null;
            if (sessionStorage && sessionStorage.getItem(languageFilePath)) {
                sessionData = JSON.parse(sessionStorage.getItem(languageFilePath));
            }

            var deferred = $q.defer();
            if (sessionData === null) {
                $resource(languageFilePath).get(function (data) {
                    if (sessionStorage) {
                        sessionStorage.setItem(languageFilePath, JSON.stringify(data));
                    }
                    deferred.resolve(data);
                });
            } else {
                deferred.resolve(sessionData);
            }
            return deferred.promise;
        }

        function setGlobalResources() {
            var deferred = $q.defer();
            resourceLoaded = false;
            var languageFilePath = "app/resources/resource" + "_" + userService.getUser().Language + '.json';
            getRecourcesFromFileOrCache(languageFilePath).then(function (data) {
                resources = data;
                resourceLoaded = true;
                deferred.resolve();
            });
            return deferred.promise;
        }

        function getResource(resourceName) {
            var result = resources[resourceName];
            if (!result)
                return resourceName;

            return result;
        }
    }
})();