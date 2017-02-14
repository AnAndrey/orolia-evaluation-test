(function () {

    var module = angular.module("app-main");
    module.controller("uploadController",  ["settings","$scope", "Upload", function(settings, $scope, Upload) {
        console.log(settings.webApiBaseUrl);

        $scope.onFileSelect = function($files) {
            Upload.upload({
                url: settings.webApiBaseUrl,
                file: $files,
            }).progress(function(e) {
            }).then(function(data, status, headers, config) {
                // file is uploaded successfully
                console.log(data);});
            }
        }]);

})();