myApp.directive('jsTreeControl', ['$log', 'CUSTOM_APP_PROPERTIES', function ($log, constants) {
    function getNodeLevel(scope, level) {
        var result = _.filter(scope.paths, function (path) {
            return path.level == level;
        });
        return result;
    }
    function getTreeJson(scope) {
        var result = { core: { data: [] } };
        var ids = [];
        console.log(scope);
        if (!scope.path)
            return;
       // angular.forEach(scope.paths, function (navigation) {

            var paths = scope.path.navigation.split('.');
            for (var i = 0; i < paths.length; i++) {
                var id = paths[i] + "_" + i.toString();
                if (ids.indexOf(id) < 0) {
                    ids.push(id);
                    if (i == 0) {
                        var item = { id: id, parent: '#', text: paths[i] }
                        result.core.data.push(item);
                    } else {
                        var item = { id: id, parent: ids[i - 1], text: paths[i] };
                        result.core.data.push(item);
                    }
                }

            }
        //});
        return result;
    }
    function link(scope, element, attrs) {
        $log.debug('jsTreeControl');
        scope.$watch('path', function (value) {
            if (value) {
                var d = getTreeJson(scope);
                var tree = $(element).jstree(d);
                tree.on('after_open.jstree', function(node) {
                    console.log(node);
                });
            }
        });

    }

    var result = {
        link: link,
        scope: {
            path: '='
        }
    };
    return result;
}]);