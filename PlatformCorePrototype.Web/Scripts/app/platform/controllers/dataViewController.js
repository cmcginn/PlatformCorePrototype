﻿myApp.controller('DataViewController',
[
    '$rootScope', '$scope', '$log', 'ngDialog', 'dataService', 'chartDataService', 'CUSTOM_APP_PROPERTIES', function($rootScope, $scope, $log, ngDialog, dataService, chartDataService, constants) {
        //helpers
        function getActiveFilters() {
            var result = [];
            angular.forEach($scope.viewDefinition.filters, function(filter) {
                var selected = _.filter(filter.filterValues, function(filterValue) {
                    return filterValue.active;
                });

                if (selected.length > 0)
                    result.push(filter);
            });
            return result;
        }
        function getData(cb) {
            var d = dataService.getLinkedListDataAsync($scope.viewDefinition.queryBuilder);
            d.then(function(data) {
                $log.debug('getDataAsync result');
                $log.debug(data);
                $scope.viewDefinition.data = data;
                $scope.$broadcast('viewDefinitionDataReceived', { viewId: $scope.viewId, data: $scope.viewDefinition.data });
                if (cb)
                    cb();
            });
        }
        function getFilterValues(cb) {
            var d = dataService.getFilterValuesAsync($scope.viewDefinition);
            d.then(function (data) {
                $log.debug('getFilterValuesAsync result');
                $scope.viewDefinition.filters = data;
                $log.debug($scope.viewDefinition.filters);
                if (cb)
                    cb();

            });
    }

    //scope
    $scope.applyFilters=function() {
        
    }
    $scope.refreshData = function () {
        var callback = function () {
            //$log.debug('Brodcasting treeDataReceived');
            //$scope.$broadcast('treeDataReceived', { viewId: $scope.viewId, data: $scope.viewDefinition.data });
        }
        getData();
    };
    $scope.init = function (viewId) {
        $scope.viewId = viewId;
        var d = dataService.getViewDefinitionAsync(viewId);
        d.then(function(data) {
            $scope.viewDefinition = data;
            $log.debug('getViewDefinitionAsync result');
            $log.debug($scope.viewDefinition);
            $scope.refreshData();
            //$scope.$broadcast('viewDefinitionReceived', {
            //    viewId: viewId,
            //    data: $scope.viewDefinition
            //});
            //getFilterValues();
        });
    };
    
    $scope.$on('filterSelectionChanged', function(e, a) {
        $log.debug('filterSelectionChanged');
        $scope.viewDefinition.queryBuilder.selectedFilters = getActiveFilters();
        $log.debug($scope.viewDefinition.queryBuilder.selectedFilters);

    });



}]);