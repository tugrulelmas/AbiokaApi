(function () {
    'use strict';

    angular.module('abioka')
      .service('userService', userService);

    /* @ngInject */
    function userService($cookies) {
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
            var isRememberMe = rememberMe || getUser().rememberMe;
            var payload = Base64.decode(token.split('.')[1]);
            var tokenUser = angular.fromJson(payload);

            user.Id = tokenUser.id;
            user.Email = tokenUser.email;
            user.Provider = tokenUser.provider;
            user.ExpirationDate = tokenUser.exp;
            user.Token = token;
            user.IsSignedIn = true;
            user.RefreshToken = tokenUser.refresh_token;
            user.Language = tokenUser.language;
            if (isRememberMe) {
                user.rememberMe = true;
                setCookie(user);
            }
            callback(user);
        }

        function updateUser(userInfo) {
            user.Language = userInfo.Language;
            if (user.rememberMe) {
                setCookie(user);
            }
        }

        function destroy() {
            var oldLanguage = user.Language;
            user = getDefault();
            user.Language = oldLanguage;
            setCookie(user);
        }

        function getDefault() {
            var browserLanguage = window.navigator.languages ? window.navigator.languages[0] : (window.navigator.userLanguage || window.navigator.language);
            return {
                Language: browserLanguage === 'tr' || browserLanguage === 'tr-TR' ? 'tr' : 'en',
                IsSignedIn: false
            };
        }

        function setCookie(user) {
            $cookies.remove('userInfo', { path: '/' });
            $cookies.putObject('userInfo', user, { path: '/' });
        }
    }
})();
