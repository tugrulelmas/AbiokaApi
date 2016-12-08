(function () {
    'use strict';

    angular.module('abioka')
      .service('userService', userService);

    /* @ngInject */
    function userService($cookies, $rootScope) {
        var user = getDefault();

        var service = {
            getUser: getUser,
            setUser: setUser,
            updateUser: updateUser,
            destroy: destroy
        };

        return service;

        function getUser() {
            var userInfo = $cookies.getObject('userInfo', { path: '/' });
            if (userInfo && userInfo.IsSignedIn === true) {
                var now = parseInt(new Date().getTime() / 1000);
                if (userInfo.ExpirationDate > now) {
                    //TODO: check if the token same as the token stored in db.
                    user = userInfo;
                } else {
                    destroy();
                    $rootScope.$broadcast('userSignedOut');
                }
            }
            return user;
        }

        function setUser(token, callback) {
            var payload = Base64.decode(token.split('.')[1]);
            var tokenUser = angular.fromJson(payload);

            user.Id = tokenUser.id;
            user.Email = tokenUser.email;
            user.Provider = tokenUser.provider;
            user.ExpirationDate = tokenUser.exp;
            user.Token = token;
            user.IsSignedIn = true;
            $cookies.putObject('userInfo', user, { path: '/' });
            callback(user);
        };

        function updateUser(userInfo) {
            user.Id = userInfo.Id;
            user.Email = userInfo.Email;
            user.Provider = userInfo.Provider;
            user.ExpirationDate = userInfo.ExpirationDate;
            user.Token = userInfo.Token;
            user.IsSignedIn = userInfo.IsSignedIn;
            $cookies.remove('userInfo', { path: '/' });
            $cookies.putObject('userInfo', user, { path: '/' });
            $rootScope.$broadcast('userUpdated');
        };

        function destroy() {
            user = getDefault();
            $cookies.remove('userInfo', { path: '/' });
            $cookies.putObject('userInfo', user, { path: '/' });
        }

        function getDefault() {
            return {
                IsSignedIn: false
            };
        };
    }
})();
