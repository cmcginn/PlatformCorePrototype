myApp.directive('treeControl', ['$log', 'CUSTOM_APP_PROPERTIES', function ($log, constants) {

  
    
    function link(scope, element, attrs) {

        scope.$on(scope.treeDataReceived, function (e, a) {
            $log.debug('treeDataReceived');
            scope.rawData = a.data;
            

        });
    }

    var result = {
        link: link,
        scope: {
            treeDataReceived:'=',
            keyProperty:'=',
            displayProperty:'='
        }
    };
    return result;
}]);