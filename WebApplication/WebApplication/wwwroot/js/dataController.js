(function () {
    "use strict";

    var module = angular.module("app-main");
    module.controller("dataController", ["settings", function (settings) {
            console.log(settings.webApiBaseUrl);
            
    }]);

})();



