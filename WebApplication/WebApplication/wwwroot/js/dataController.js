(function () {
    "use strict";

    var module = angular.module("app-main");
    
    module.directive('myElem',
       function () {
           return {
               restrict: 'E',
               replace: true,

               template: '<div id="chartdiv" style="min-width: 410px; height: 500px; margin: 0 auto"></div>',
               link: function (scope, element, attrs) {
                   var chart = false;

               }//end watch           
           }
       });

})();



