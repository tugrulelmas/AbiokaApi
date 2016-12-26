(function () {
    'use strict';

    angular.module('abioka')
      .filter('stringFormat', stringFormat);

    function stringFormat() {
        return function (input) {
            if (arguments.length > 1) {
                // If we have more than one argument (insertion values have been given)
                var str = input;
                // Loop through the values we have been given to insert
                for (var i = 1; i < arguments.length; i++) {
                    // Compile a new Regular expression looking for {0}, {1} etc in the input string
                    var reg = new RegExp("\\{" + (i - 1) + "\\}");
                    // Perform the replace with the compiled RegEx and the value
                    str = str.replace(reg, arguments[i]);
                }
                return str;
            }

            return input;
        };
    }
})();