(function () {
    'use strict';

    angular.module('abioka')
      .service('userService', userService);

    /* @ngInject */
    function userService($cookies, $rootScope) {
        var user = getDefault();
        var rememberMe = false;

        var service = {
            getUser: getUser,
            setUser: setUser,
            updateUser: updateUser,
            destroy: destroy,
            setRememberMe: setRememberMe
        };

        return service;

        function setRememberMe(remember) {
            rememberMe = remember;
        }

        function getUser() {
            var userInfo = $cookies.getObject('userInfo', { path: '/' });
            if (userInfo && userInfo.IsSignedIn === true) {
                user = userInfo;
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
            user.RefreshToken = tokenUser.refresh_token;
            if (angular.isUndefined(user.Language) || user.Language.trim() === "") {
                user.Language = getDefault().Language;
            }
            setCookie(user);
            callback(user);
        }

        function updateUser(userInfo) {
            user.Id = userInfo.Id;
            user.Email = userInfo.Email;
            user.Provider = userInfo.Provider;
            user.ExpirationDate = userInfo.ExpirationDate;
            user.Token = userInfo.Token;
            user.IsSignedIn = userInfo.IsSignedIn;
            user.Language = userInfo.Language;
            setCookie(user);
            $rootScope.$broadcast('userUpdated');
        }

        function destroy() {
            var oldLanguage = user.Language;
            user = getDefault();
            user.Language = oldLanguage;
            setCookie(user);
        }

        function getDefault() {
            return {
                Language: "en",
                IsSignedIn: false
            };
        }

        function setCookie(user) {
            if (rememberMe) {
                $cookies.remove('userInfo', { path: '/' });
                $cookies.putObject('userInfo', user, { path: '/' });
            }
        }
    }
})();
