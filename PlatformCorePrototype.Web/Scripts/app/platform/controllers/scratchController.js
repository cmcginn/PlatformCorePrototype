myApp.controller('ScratchController', ['$scope', '$timeout', '$resource','dataService', function ($scope, $timeout, $resource,dataservice) {

    $scope.accounts = [];
    $scope.salesPersons = [];
    $scope.products = [];
  //function getDataByPath()
    $scope.getDataByPath = function(path) {
        var d = dataservice.getScratchDataAsync(path);
        d.then(function (data) {
            if (path == "Account") {
     
                $scope.accounts = [];
                angular.forEach(data, function (i) {

                    var item = { id: i._id.slicer_0, sales: i.measure_0 };
                    $scope.accounts.push(item);
                });
            } else if (path == "Account_SalesPerson") {
                $scope.salesPersons = [];
                angular.forEach(data, function(i) {
                    var item = { id: i._id.slicer_0, name: i._id.slicer_1, sales: i.measure_0 };
                    $scope.salesPersons.push(item);
                });
            
            } else if (path == "Account_SalesPerson_Product") {
                $scope.products = [];
                angular.forEach(data, function (i) {
                    var item = { id: i._id.slicer_0, salesPerson: i._id.slicer_1,name:i._id.slicer_2, sales: i.measure_0 };
                    $scope.products.push(item);
                });

            }
            //console.log(data);
        });
    }
}]);