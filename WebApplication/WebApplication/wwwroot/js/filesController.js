(function () {

    "use strict";

    var module = angular.module("app-main");
    module.controller("filesController", ["settings", "$scope", function (settings, $scope) {
            
        $scope.refresh = function () {
            $.getJSON(settings.fileServiceUrl,
            {},
            function (json) {
                var data = [];
                $.each(json,
                    function (i, val) { 
                        data.push(
                            { File: val, Delete: '', FileServiceUrl: settings.fileServiceUrl, DataServiceUrl: settings.dataServiceUrl });
                    });

                $scope.gridOptions.data = data;
            });
        }
            
        var rowSelected = function (row) {
            var url = row.entity.DataServiceUrl + row.entity.File;

            $.getJSON(url,
            {},
            function (json) {

                var chart = AmCharts.makeChart("chartdiv", {
                    "type": "serial",
                    "theme": "none",
                    "marginLeft": 20,
                    "pathToImages": "http://www.amcharts.com/lib/3/images/",
                    "dataProvider": json,
                    "valueAxes": [{
                        "axisAlpha": 0,
                        "inside": true,
                        "position": "left",
                        "ignoreAxisWidth": true
                    }],
                    "graphs": [{
                        "balloonText": "[[category]]<br><b><span style='font-size:14px;'>[[value]]</span></b>",
                        "bullet": "round",
                        "bulletSize": 6,
                        "lineColor": "#d1655d",
                        "lineThickness": 2,
                        "negativeLineColor": "#637bb6",
                        "type": "line",
                        "valueField": "value"
                    }],
                    "chartScrollbar": {},
                    "chartCursor": {
                        "cursorAlpha": 0,
                        "cursorPosition": "mouse"
                    },
                    "categoryField": "mark",
                    "categoryAxis": {
                        "parseDates": false,
                        "minorGridAlpha": 0.1,
                        "minorGridEnabled": true,
                        "labelRotation": 90
                    }
                });
            });

        }

        $scope.gridOptions = {
            enableColumnMenus: false,
            enableColumnResizing: true,
            enableRowSelection: true, 
            enableRowHeaderSelection: false,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                gridApi.cellNav.on.navigate($scope, function (newRowCol, oldRowCol) {
                    rowSelected(newRowCol.row);
                });
            },
            multiSelect: false,
            odifierKeysToMultiSelect :false,
            noUnselect: true
        };

        $scope.deleteRow = function (row) {
            if (confirm("Are you sure you want to delete the '" + row.entity.File + "'?")) {
                var param = encodeURIComponent(row.entity.File);
                $.ajax({
                    url: settings.fileServiceUrl + param,
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
            else {alertify.error("Operation is cancelled.");}
        };

        $scope.gridOptions.columnDefs = [{ name: 'File' },
            {
                name: 'Delete',
                cellTemplate: '<button class="btn primary" ng-click="grid.appScope.deleteRow(row)">Delete</button>',
                width: 67,
                allowCellFocus: false,
                enableSorting: false
            }];
        $scope.refresh();
    }]);

})();