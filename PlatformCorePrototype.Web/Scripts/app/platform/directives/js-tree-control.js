myApp.directive('jsTreeControl', ['$log','CUSTOM_APP_PROPERTIES', function($log,constants) {
    function getNodeLevel(scope, level) {
        var result = _.filter(scope.paths, function(path) {
            return path.level == level;
        });
        return result;
    }
    function getTreeJson(scope) {
        
        $log.debug('jsTreeControl getTreeJson');
        console.log(scope.paths);
        //var minLevel = d3.min(scope.paths, function (val) { return val.level; });
        var maxLevel = d3.max(scope.paths, function (val) { return val.level; });
        
        //var level = 0;
        //var roots = getNodeLevel(scope, level);
        var result = { core: { data: [] } };
        var root = {
            'text': scope.paths[0].displayName,
            'state': {
                'opened': false,
                'selected': false
            },
            'children':[]
        };
        var current = root;
        for (var i = 1; i < maxLevel; i++) {
            var items = _.filter(scope.paths, function(path) {
                return path.level == i;
            });
            angular.forEach(items, function (item) {
                var node = { text: item.displayName,children:[] };
                current.children.push(node);
                current = item;
            });
        }
        result.core.data = root;
        return result;
    }
    function link(scope, element, attrs) {
        $log.debug('jsTreeControl');
        // scope.paths = scope.paths;
        scope.$watch('paths', function(value) {
            if (value) {
                var d = getTreeJson(scope);
                $(element).jstree(d);
            }
        });
        //scope.$watch('paths', function(o, n) {
        //    console.log('watcher');
        //    console.log(n);
        //});
        // $log.debug(scope);
      
        $log.debug('jsTreeControl end');
    }

    var result = {
        link: link,
        scope: {
            paths:'='
        }
    };
    return result;
}]);