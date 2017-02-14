(function () {

    var module = angular.module("app-main");
    module.controller("uploadController", ["settings", "$scope", "Upload", function (settings, $scope, Upload) {
        console.log(settings.webApiBaseUrl);

        $scope.onFileSelect = function ($files, gridOptions) {
            Upload.upload({
                url: settings.webApiBaseUrl,
                file: $files,
            }).progress(function(e) {
            }).then(function(data, status, headers, config) {
                if (data.data.count !== 0) {
                    $.each(data.data.copiedFiles,
                        function (i, file) { 
                            console.log(file);
                            gridOptions.data.splice(0, 0, { File: file, Delete: "" });
                            alertify.success("Successfully uploaded.");
                        });
                }
            }, function (resp) {
                alertify.success("Upload failed.");
            });
        }
    }]);

})();