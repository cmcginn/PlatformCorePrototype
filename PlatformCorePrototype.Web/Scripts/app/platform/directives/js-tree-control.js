myApp.directive('jsTreeControl', ['$log', 'CUSTOM_APP_PROPERTIES', function ($log, constants) {
    var el = null;
    function buildTree(scope) {
        var mapped = d3.nest()
            .key(function(d) { return d.navigation })
            .map(scope.map, d3.map);

        var stage1 = [];
        angular.forEach(mapped.entries(), function(entry) {
            var keys = entry.key.split('.');
            for (var i = 0; i < keys.length; i++) {
                var item = { id: keys[i] + '_' + i.toString(), text: keys[i], parent:'#' };
                if (i > 0)
                    item.parent = keys[i - 1] + '_' + (i - 1).toString();
                
                  
                stage1.push(item);

            }
        });
        var result = [];
        angular.forEach(stage1, function(stage) {
            var existing = _.filter(result, function(resultValue) {
                return resultValue.id == stage.id;
            });
            if (existing.length == 0)
                result.push(stage);

        });
        console.log(result);
        el.jstree({ core: {data:result} });

    }


    function link(scope, element, attrs) {
        
        el = $(element);
        console.log(el);
        $log.debug('jsTreeControl');
        scope.$watch('map', function (value) {
            if (value) {
                scope.map = value;
                buildTree(scope);
            }
        });

    }

    var result = {
        link: link,
        scope: {
            map: '=',
            id:'='
        }
    };
    return result;
}]);