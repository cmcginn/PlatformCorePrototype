myApp.directive('objectPropertiesTableControl', ['CUSTOM_APP_PROPERTIES','$log',function(constants,$log) {

    function link(scope, element, attrs) {
        $log.debug('linking objectPropertiesTableControl');
        $log.debug(scope);
        scope.$on('viewDefinitionDataReceived', function(e, a) {
            $log.debug('objectPropertiesTableControl viewDefinitionDataReceived');
            var data = a.data;
            if (data.length > 0) {
      
                scope.keys = Object.keys(data[0]);
              
                scope.items = [];
                angular.forEach(data, function(item) {
                    var dataItem = [];
                    angular.forEach(Object.keys(item), function(key) {
                        dataItem.push({ key: key, value: item[key] });
                    });
                    scope.items.push(dataItem);

                });
            }
        });
    }

    var result = {
        link: link,
        templateUrl: constants.templateBasePath + '/object-properties-table-control.html',
        scope: {
            viewId: '='
        }
    };
    return result;
}]);