(function () {
    'use strict';

    angular.module('abioka')
      .directive('gridCell', ['$parse', '$compile', '$filter', function ($parse, $compile, $filter) {
          var uiGridCell = {
              priority: 0,
              scope: false,
              compile: function () {
                  return {
                      pre: function ($scope, $elm, $attrs, uiGridCtrl) {
                          if (!$scope.column.cellTemplate) {
                              var getter = $parse($scope.column.name);
                              var result = getter($scope.$parent.entity);
                              if ($scope.column.filter) {
                                  result = $filter($scope.column.filter)(result);
                              }

                              var appendedResult = result ? result.toString() : "";
                              $elm.append(appendedResult);
                          } else {
                              var cellElement = $compile($scope.column.cellTemplate)($scope.$parent);
                              $elm.append(cellElement);
                          }
                      }
                  };
              }
          };

          return uiGridCell;
      }]);
})();