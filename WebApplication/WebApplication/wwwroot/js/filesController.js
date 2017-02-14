(function () {

    "use strict";

    var module = angular.module("app-main");
    module.controller("filesController", ["settings", "$scope", function (settings, $scope) {
            
            $scope.refresh = function () {
                $.getJSON(settings.webApiBaseUrl,
                {},
                function (json) {
                    var data = [];
                    $.each(json,
                        function (i, val) { // обрабатываем полученные данные
                            console.log(val);
                            data.push(
                                { File: val, Delete: '' });
                        });

                    $scope.gridOptions.data = data;
                });
            }


            $scope.gridOptions = {
                // comment the next line to enable column reordering/moving feature
                enableColumnMenus: false,
                enableColumnResizing: true,
                onRegisterApi: function (gridApi) { $scope.gridApi = gridApi; }
            };

            $scope.deleteRow = function (row) {

                if (confirm("Are you sure you want to delete the '" + row.entity.File + "'?")) {

                    var param = encodeURIComponent(row.entity.File);
                    $.ajax({
                        url: settings.webApiBaseUrl + param,
                        type: 'DELETE',
                        success: function (result) {
                            var index = $scope.gridOptions.data.indexOf(row.entity);
                            $scope.gridOptions.data.splice(index, 1);
                            $scope.gridApi.core.refresh();
                            alertify.success("Record is deleted.");
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            alertify.error("Operation is failed. ");;
                        }
                    });


                }
                else {
                    alertify.error("Operation is cancelled.");
                }
                // });
            };

            $scope.gridOptions.columnDefs = [{
                name: 'File'
            }, {
                name: 'Delete',
                cellTemplate: '<button class="btn primary" ng-click="grid.appScope.deleteRow(row)">Delete</button>',
                width: 67
            }];



            console.log(settings.webApiBaseUrl);
            $scope.refresh();

    }]);

})();