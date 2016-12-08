(function () {
	'use strict';

	angular.module('abioka')
	  .config(config);

	/* @ngInject */
	function config($httpProvider) {
		$httpProvider.interceptors.push('tokenInjector');
		$httpProvider.interceptors.push('errorInjector');
	}
})();