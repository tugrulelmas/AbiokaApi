(function () {
    'use strict';

    angular.module('abioka').factory('UserResource', userResource);

    /* @ngInject */
    function userResource($resource) {
        return {
            users: $resource('./user/:id', null, {
                'update': { method:'PUT' }
            })
        };
    }

})();