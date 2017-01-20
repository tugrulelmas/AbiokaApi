/**
 * Satellizer 0.15.5
 * (c) 2016 Sahat Yalkabov 
 * License: MIT 
 */

(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
    typeof define === 'function' && define.amd ? define(factory) :
    (global.satellizer = factory());
}(this, function () {
    'use strict';

    var Config = (function () {
        function Config() {
            this.baseUrl = '/';
            this.unlinkUrl = '/auth/unlink/';
            this.tokenName = 'token';
            this.tokenRoot = null;
            this.withCredentials = false;
            this.providers = {
                facebook: {
                    name: 'facebook',
                    url: '/auth/facebook',
                    authorizationEndpoint: 'https://www.facebook.com/v2.5/dialog/oauth',
                    redirectUri: window.location.origin + '/',
                    requiredUrlParams: ['display', 'scope'],
                    scope: ['email'],
                    scopeDelimiter: ',',
                    display: 'popup',
                    oauthType: '2.0',
                    popupOptions: { width: 580, height: 400 }
                },
                google: {
                    name: 'google',
                    url: '/auth/google',
                    authorizationEndpoint: 'https://accounts.google.com/o/oauth2/auth',
                    redirectUri: window.location.origin,
                    requiredUrlParams: ['scope'],
                    optionalUrlParams: ['display', 'state'],
                    scope: ['profile', 'email'],
                    scopePrefix: 'openid',
                    scopeDelimiter: ' ',
                    display: 'popup',
                    oauthType: '2.0',
                    popupOptions: { width: 452, height: 633 },
                    state: function () { return encodeURIComponent(Math.random().toString(36).substr(2)); }
                },
                github: {
                    name: 'github',
                    url: '/auth/github',
                    authorizationEndpoint: 'https://github.com/login/oauth/authorize',
                    redirectUri: window.location.origin,
                    optionalUrlParams: ['scope'],
                    scope: ['user:email'],
                    scopeDelimiter: ' ',
                    oauthType: '2.0',
                    popupOptions: { width: 1020, height: 618 }
                },
                instagram: {
                    name: 'instagram',
                    url: '/auth/instagram',
                    authorizationEndpoint: 'https://api.instagram.com/oauth/authorize',
                    redirectUri: window.location.origin,
                    requiredUrlParams: ['scope'],
                    scope: ['basic'],
                    scopeDelimiter: '+',
                    oauthType: '2.0'
                },
                linkedin: {
                    name: 'linkedin',
                    url: '/auth/linkedin',
                    authorizationEndpoint: 'https://www.linkedin.com/uas/oauth2/authorization',
                    redirectUri: window.location.origin,
                    requiredUrlParams: ['state'],
                    scope: ['r_emailaddress'],
                    scopeDelimiter: ' ',
                    state: 'STATE',
                    oauthType: '2.0',
                    popupOptions: { width: 527, height: 582 }
                },
                twitter: {
                    name: 'twitter',
                    url: '/auth/twitter',
                    authorizationEndpoint: 'https://api.twitter.com/oauth/authenticate',
                    redirectUri: window.location.origin,
                    oauthType: '1.0',
                    popupOptions: { width: 495, height: 645 }
                },
                twitch: {
                    name: 'twitch',
                    url: '/auth/twitch',
                    authorizationEndpoint: 'https://api.twitch.tv/kraken/oauth2/authorize',
                    redirectUri: window.location.origin,
                    requiredUrlParams: ['scope'],
                    scope: ['user_read'],
                    scopeDelimiter: ' ',
                    display: 'popup',
                    oauthType: '2.0',
                    popupOptions: { width: 500, height: 560 }
                },
                live: {
                    name: 'live',
                    url: '/auth/live',
                    authorizationEndpoint: 'https://login.live.com/oauth20_authorize.srf',
                    redirectUri: window.location.origin,
                    requiredUrlParams: ['display', 'scope'],
                    scope: ['wl.emails'],
                    scopeDelimiter: ' ',
                    display: 'popup',
                    oauthType: '2.0',
                    popupOptions: { width: 500, height: 560 }
                },
                yahoo: {
                    name: 'yahoo',
                    url: '/auth/yahoo',
                    authorizationEndpoint: 'https://api.login.yahoo.com/oauth2/request_auth',
                    redirectUri: window.location.origin,
                    scope: [],
                    scopeDelimiter: ',',
                    oauthType: '2.0',
                    popupOptions: { width: 559, height: 519 }
                },
                bitbucket: {
                    name: 'bitbucket',
                    url: '/auth/bitbucket',
                    authorizationEndpoint: 'https://bitbucket.org/site/oauth2/authorize',
                    redirectUri: window.location.origin + '/',
                    requiredUrlParams: ['scope'],
                    scope: ['email'],
                    scopeDelimiter: ' ',
                    oauthType: '2.0',
                    popupOptions: { width: 1028, height: 529 }
                },
                spotify: {
                    name: 'spotify',
                    url: '/auth/spotify',
                    authorizationEndpoint: 'https://accounts.spotify.com/authorize',
                    redirectUri: window.location.origin,
                    optionalUrlParams: ['state'],
                    requiredUrlParams: ['scope'],
                    scope: ['user-read-email'],
                    scopePrefix: '',
                    scopeDelimiter: ',',
                    oauthType: '2.0',
                    popupOptions: { width: 500, height: 530 },
                    state: function () { return encodeURIComponent(Math.random().toString(36).substr(2)); }
                }
            };
            this.httpInterceptor = function () { return true; };
        }
        Object.defineProperty(Config, "getConstant", {
            get: function () {
                return new Config();
            },
            enumerable: true,
            configurable: true
        });
        return Config;
    }());
    ;

    var AuthProvider = (function () {
        function AuthProvider(SatellizerConfig) {
            this.SatellizerConfig = SatellizerConfig;
        }
        Object.defineProperty(AuthProvider.prototype, "baseUrl", {
            get: function () { return this.SatellizerConfig.baseUrl; },
            set: function (value) { this.SatellizerConfig.baseUrl = value; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(AuthProvider.prototype, "unlinkUrl", {
            get: function () { return this.SatellizerConfig.unlinkUrl; },
            set: function (value) { this.SatellizerConfig.unlinkUrl = value; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(AuthProvider.prototype, "tokenName", {
            get: function () { return this.SatellizerConfig.tokenName; },
            set: function (value) { this.SatellizerConfig.tokenName = value; },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(AuthProvider.prototype, "withCredentials", {
            get: function () { return this.SatellizerConfig.withCredentials; },
            set: function (value) { this.SatellizerConfig.withCredentials = value; },
            enumerable: true,
            configurable: true
        });
        AuthProvider.prototype.facebook = function (options) {
            angular.extend(this.SatellizerConfig.providers.facebook, options);
        };
        AuthProvider.prototype.google = function (options) {
            angular.extend(this.SatellizerConfig.providers.google, options);
        };
        AuthProvider.prototype.github = function (options) {
            angular.extend(this.SatellizerConfig.providers.github, options);
        };
        AuthProvider.prototype.instagram = function (options) {
            angular.extend(this.SatellizerConfig.providers.instagram, options);
        };
        AuthProvider.prototype.linkedin = function (options) {
            angular.extend(this.SatellizerConfig.providers.linkedin, options);
        };
        AuthProvider.prototype.twitter = function (options) {
            angular.extend(this.SatellizerConfig.providers.twitter, options);
        };
        AuthProvider.prototype.twitch = function (options) {
            angular.extend(this.SatellizerConfig.providers.twitch, options);
        };
        AuthProvider.prototype.live = function (options) {
            angular.extend(this.SatellizerConfig.providers.live, options);
        };
        AuthProvider.prototype.yahoo = function (options) {
            angular.extend(this.SatellizerConfig.providers.yahoo, options);
        };
        AuthProvider.prototype.bitbucket = function (options) {
            angular.extend(this.SatellizerConfig.providers.bitbucket, options);
        };
        AuthProvider.prototype.spotify = function (options) {
            angular.extend(this.SatellizerConfig.providers.spotify, options);
        };
        AuthProvider.prototype.oauth2 = function (options) {
            this.SatellizerConfig.providers[options.name] = angular.extend(options, {
                oauthType: '2.0'
            });
        };
        AuthProvider.prototype.$get = function (SatellizerOAuth) {
            return {
                authenticate: function (name, data) { return SatellizerOAuth.authenticate(name, data); },
                link: function (name, data) { return SatellizerOAuth.authenticate(name, data); },
                unlink: function (name, options) { return SatellizerOAuth.unlink(name, options); }
            };
        };
        AuthProvider.$inject = ['SatellizerConfig'];
        return AuthProvider;
    }());
    AuthProvider.prototype.$get.$inject = ['SatellizerOAuth'];

    function joinUrl(baseUrl, url) {
        if (/^(?:[a-z]+:)?\/\//i.test(url)) {
            return url;
        }
        var joined = [baseUrl, url].join('/');
        var normalize = function (str) {
            return str
                .replace(/[\/]+/g, '/')
                .replace(/\/\?/g, '?')
                .replace(/\/\#/g, '#')
                .replace(/\:\//g, '://');
        };
        return normalize(joined);
    }
    function getFullUrlPath(location) {
        var isHttps = location.protocol === 'https:';
        return location.protocol + '//' + location.hostname +
            ':' + (location.port || (isHttps ? '443' : '80')) +
            (/^\//.test(location.pathname) ? location.pathname : '/' + location.pathname);
    }
    function parseQueryString(str) {
        var obj = {};
        var key;
        var value;
        angular.forEach((str || '').split('&'), function (keyValue) {
            if (keyValue) {
                value = keyValue.split('=');
                key = decodeURIComponent(value[0]);
                obj[key] = angular.isDefined(value[1]) ? decodeURIComponent(value[1]) : true;
            }
        });
        return obj;
    }

    var Popup = (function () {
        function Popup($interval, $window, $q) {
            this.$interval = $interval;
            this.$window = $window;
            this.$q = $q;
            this.popup = null;
            this.defaults = {
                redirectUri: null
            };
        }
        Popup.prototype.stringifyOptions = function (options) {
            var parts = [];
            angular.forEach(options, function (value, key) {
                parts.push(key + '=' + value);
            });
            return parts.join(',');
        };
        Popup.prototype.open = function (url, name, popupOptions, redirectUri, dontPoll) {
            var width = popupOptions.width || 500;
            var height = popupOptions.height || 500;
            var options = this.stringifyOptions({
                width: width,
                height: height,
                top: this.$window.screenY + ((this.$window.outerHeight - height) / 2.5),
                left: this.$window.screenX + ((this.$window.outerWidth - width) / 2)
            });
            var popupName = this.$window['cordova'] || this.$window.navigator.userAgent.indexOf('CriOS') > -1 ? '_blank' : name;
            this.popup = this.$window.open(url, popupName, options);
            if (this.popup && this.popup.focus) {
                this.popup.focus();
            }
            if (dontPoll) {
                return;
            }
            if (this.$window['cordova']) {
                return this.eventListener(redirectUri);
            }
            else {
                if (url === 'about:blank') {
                    this.popup.location = url;
                }
                return this.polling(redirectUri);
            }
        };
        Popup.prototype.polling = function (redirectUri) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                var redirectUriParser = document.createElement('a');
                redirectUriParser.href = redirectUri;
                var redirectUriPath = getFullUrlPath(redirectUriParser);
                var polling = _this.$interval(function () {
                    if (!_this.popup || _this.popup.closed || _this.popup.closed === undefined) {
                        _this.$interval.cancel(polling);
                        reject(new Error('The popup window was closed'));
                    }
                    try {
                        var popupWindowPath = getFullUrlPath(_this.popup.location);
                        if (popupWindowPath === redirectUriPath) {
                            if (_this.popup.location.search || _this.popup.location.hash) {
                                var query = parseQueryString(_this.popup.location.search.substring(1).replace(/\/$/, ''));
                                var hash = parseQueryString(_this.popup.location.hash.substring(1).replace(/[\/$]/, ''));
                                var params = angular.extend({}, query, hash);
                                if (params.error) {
                                    reject(new Error(params.error));
                                }
                                else {
                                    resolve(params);
                                }
                            }
                            else {
                                reject(new Error('OAuth redirect has occurred but no query or hash parameters were found. ' +
                                    'They were either not set during the redirect, or were removed—typically by a ' +
                                    'routing library—before Satellizer could read it.'));
                            }
                            _this.$interval.cancel(polling);
                            _this.popup.close();
                        }
                    }
                    catch (error) {
                    }
                }, 500);
            });
        };
        Popup.prototype.eventListener = function (redirectUri) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                _this.popup.addEventListener('loadstart', function (event) {
                    if (event.url.indexOf(redirectUri) !== 0) {
                        return;
                    }
                    var parser = document.createElement('a');
                    parser.href = event.url;
                    if (parser.search || parser.hash) {
                        var query = parseQueryString(parser.search.substring(1).replace(/\/$/, ''));
                        var hash = parseQueryString(parser.hash.substring(1).replace(/[\/$]/, ''));
                        var params = angular.extend({}, query, hash);
                        if (params.error) {
                            reject(new Error(params.error));
                        }
                        else {
                            resolve(params);
                        }
                        _this.popup.close();
                    }
                });
                _this.popup.addEventListener('loaderror', function () {
                    reject(new Error('Authorization failed'));
                });
                _this.popup.addEventListener('exit', function () {
                    reject(new Error('The popup window was closed'));
                });
            });
        };
        Popup.$inject = ['$interval', '$window', '$q'];
        return Popup;
    }());

    var OAuth2 = (function () {
        function OAuth2($http, $window, $timeout, $q, SatellizerConfig, SatellizerPopup) {
            this.$http = $http;
            this.$window = $window;
            this.$timeout = $timeout;
            this.$q = $q;
            this.SatellizerConfig = SatellizerConfig;
            this.SatellizerPopup = SatellizerPopup;
            this.defaults = {
                name: null,
                url: null,
                clientId: null,
                authorizationEndpoint: null,
                redirectUri: null,
                scope: null,
                scopePrefix: null,
                scopeDelimiter: null,
                state: null,
                requiredUrlParams: null,
                defaultUrlParams: ['response_type', 'client_id', 'redirect_uri'],
                responseType: 'code',
                responseParams: {
                    code: 'code',
                    clientId: 'clientId',
                    redirectUri: 'redirectUri'
                },
                oauthType: '2.0',
                popupOptions: { width: null, height: null }
            };
        }
        OAuth2.camelCase = function (name) {
            return name.replace(/([\:\-\_]+(.))/g, function (_, separator, letter, offset) {
                return offset ? letter.toUpperCase() : letter;
            });
        };
        OAuth2.prototype.init = function (options, userData) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                angular.extend(_this.defaults, options);
                var stateName = _this.defaults.name + '_state';
                var _a = _this.defaults, name = _a.name, state = _a.state, popupOptions = _a.popupOptions, redirectUri = _a.redirectUri, responseType = _a.responseType;
                var url = [_this.defaults.authorizationEndpoint, _this.buildQueryString()].join('?');
                _this.SatellizerPopup.open(url, name, popupOptions, redirectUri).then(function (oauth) {
                    if (responseType === 'token' || !url) {
                        return resolve(oauth);
                    }
                    resolve(_this.exchangeForToken(oauth, userData));
                }).catch(function (error) { return reject(error); });
            });
        };
        OAuth2.prototype.exchangeForToken = function (oauthData, userData) {
            var _this = this;
            var payload = angular.extend({}, userData);
            angular.forEach(this.defaults.responseParams, function (value, key) {
                switch (key) {
                    case 'code':
                        payload[value] = oauthData.code;
                        break;
                    case 'clientId':
                        payload[value] = _this.defaults.clientId;
                        break;
                    case 'redirectUri':
                        payload[value] = _this.defaults.redirectUri;
                        break;
                    default:
                        payload[value] = oauthData[key];
                }
            });
            if (oauthData.state) {
                payload.state = oauthData.state;
            }
            var exchangeForTokenUrl = this.SatellizerConfig.baseUrl ?
                joinUrl(this.SatellizerConfig.baseUrl, this.defaults.url) :
                this.defaults.url;
            return this.$http.post(exchangeForTokenUrl, payload, { withCredentials: this.SatellizerConfig.withCredentials });
        };
        OAuth2.prototype.buildQueryString = function () {
            var _this = this;
            var keyValuePairs = [];
            var urlParamsCategories = ['defaultUrlParams', 'requiredUrlParams', 'optionalUrlParams'];
            angular.forEach(urlParamsCategories, function (paramsCategory) {
                angular.forEach(_this.defaults[paramsCategory], function (paramName) {
                    var camelizedName = OAuth2.camelCase(paramName);
                    var paramValue = angular.isFunction(_this.defaults[paramName]) ? _this.defaults[paramName]() : _this.defaults[camelizedName];
                    if (paramName === 'redirect_uri' && !paramValue) {
                        return;
                    }
                    if (paramName === 'state') {
                        paramValue = encodeURIComponent(Math.random().toString(36).substr(2));
                    }
                    if (paramName === 'scope' && Array.isArray(paramValue)) {
                        paramValue = paramValue.join(_this.defaults.scopeDelimiter);
                        if (_this.defaults.scopePrefix) {
                            paramValue = [_this.defaults.scopePrefix, paramValue].join(_this.defaults.scopeDelimiter);
                        }
                    }
                    keyValuePairs.push([paramName, paramValue]);
                });
            });
            return keyValuePairs.map(function (pair) { return pair.join('='); }).join('&');
        };
        OAuth2.$inject = ['$http', '$window', '$timeout', '$q', 'SatellizerConfig', 'SatellizerPopup'];
        return OAuth2;
    }());

    var OAuth = (function () {
        function OAuth($http, $window, $timeout, $q, SatellizerConfig, SatellizerPopup, SatellizerOAuth2) {
            this.$http = $http;
            this.$window = $window;
            this.$timeout = $timeout;
            this.$q = $q;
            this.SatellizerConfig = SatellizerConfig;
            this.SatellizerPopup = SatellizerPopup;
            this.SatellizerOAuth2 = SatellizerOAuth2;
        }
        OAuth.prototype.authenticate = function (name, userData) {
            var _this = this;
            return this.$q(function (resolve, reject) {
                var provider = _this.SatellizerConfig.providers[name];
                var oauth = new OAuth2(_this.$http, _this.$window, _this.$timeout, _this.$q, _this.SatellizerConfig, _this.SatellizerPopup);
                return oauth.init(provider, userData).then(function (response) {
                    resolve(response);
                }).catch(function (error) {
                    reject(error);
                });
            });
        };
        OAuth.prototype.unlink = function (provider, httpOptions) {
            if (httpOptions === void 0) { httpOptions = {}; }
            httpOptions.url = httpOptions.url ? httpOptions.url : joinUrl(this.SatellizerConfig.baseUrl, this.SatellizerConfig.unlinkUrl);
            httpOptions.data = { provider: provider } || httpOptions.data;
            httpOptions.method = httpOptions.method || 'POST';
            httpOptions.withCredentials = httpOptions.withCredentials || this.SatellizerConfig.withCredentials;
            return this.$http(httpOptions);
        };
        OAuth.$inject = [
            '$http',
            '$window',
            '$timeout',
            '$q',
            'SatellizerConfig',
            'SatellizerPopup',
            'SatellizerOAuth2'
        ];
        return OAuth;
    }());

    angular.module('satellizer', [])
        .provider('$auth', ['SatellizerConfig', function (SatellizerConfig) { return new AuthProvider(SatellizerConfig); }])
        .constant('SatellizerConfig', Config.getConstant)
        .service('SatellizerPopup', Popup)
        .service('SatellizerOAuth', OAuth)
        .service('SatellizerOAuth2', OAuth2);
    var ng1 = 'satellizer';

    return ng1;

}));