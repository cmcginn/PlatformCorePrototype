myApp.directive('jsTreeControl', ['$log', 'CUSTOM_APP_PROPERTIES', function ($log, constants) {
    var el = null;
    function buildTree(scope) {
        var mapped = d3.nest()
            .key(function(d) { return d.navigation })
            .map(scope.map, d3.map);
        
        var stage1 = [];
        angular.forEach(mapped.entries(), function (entry) {
         
            var keys = entry.key.split('.');
            var parent = null;
            for (var i = 0; i < keys.length; i++) {

                var item = { id: keys[i] + '_' + i.toString(), text: keys[i], parent: '#', data: { path: entry.key, level: i } };
                if (i > 0)
                    item.parent = keys[i - 1] + '_' + (i - 1).toString();
                stage1.push(item);
                parent = item;
            }
            
            for (var i = 0; i < entry.value.length; i++) {
          
                var item = {
                    id: parent.id + '_' + i.toString() + '_leaf', text: entry.value[i].key.toString(), parent: parent.id,icon:'folder-icon',data: { path: entry.key, level: keys.length - 1 }};
                stage1.push(item);
            }
        });
        var result = [];
    
        angular.forEach(stage1, function (stage) {
      
            var existing = _.filter(result, function(resultValue) {
                return resultValue.id == stage.id;
            });
            if (existing.length == 0)
                result.push(stage);

        });
       
        var tree = el.jstree({
            core: { data: result }
        });
        tree.on('after_open.jstree', function(e, data) {
            scope.selected_node = data.node.data;
            scope.$emit('tree_node_selected', scope.selected_node);
        });
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