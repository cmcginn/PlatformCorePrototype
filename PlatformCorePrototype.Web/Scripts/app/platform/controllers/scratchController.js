myApp.controller('ScratchController', ['$scope', '$timeout', '$resource','dataService', function ($scope, $timeout, $resource,dataservice) {

    //function buildTree(map) {
    //    var mapped = d3.nest()
    //        .key(function (d) { return d.navigation })
    //        .map(map, d3.map);

    //    var stage1 = [];
    //    angular.forEach(mapped.entries(), function (entry) {
    //        var keys = entry.key.split('.');
    //        for (var i = 0; i < keys.length; i++) {
    //            var item = { id: keys[i] + '_' + i.toString(), text: keys[i] };
    //            if (i > 0) 
    //                item.parent = keys[i - 1] + '_' + (i - 1).toString();
    //            stage1.push(item);

    //        }
    //    });
    //    var result = [];
    //    angular.forEach(stage1, function(stage) {
    //        var existing = _.filter(result, function(resultValue) {
    //            return resultValue.id == stage.id;
    //        });
    //        if (existing.length == 0)
    //            result.push(stage);
            
    //    });
        
    //}
    //function buildTrees() {
    //    angular.forEach($scope.linkedListMaps, function(linkListMap) {
    //        buildTree(linkListMap.navigationMaps);
    //    });
    //}

    $scope.linkedListMaps = [];
    $scope.init=function(viewId) {
        var d = dataservice.getScratchDataAsync(viewId);
        d.then(function (data) {
            $scope.linkedListMaps = data.linkedListMaps;
            console.log($scope.linkedListMaps[0]);
        });
    }
}]);