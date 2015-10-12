myApp.service('dataService', ['$http', '$q', 'CUSTOM_APP_PROPERTIES', function($http, $q, appProperties) {

    var result = {
        getViewDefinitionAsync: function(id) {

            return $q(function (resolve, reject) {
           
                $http.get(appProperties.apiBasePath + '/ViewDefinition/' + id).
                    success(function(data, status, headers, config) {
                        resolve(data);
                    })
                    .error(function(data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    });
            });
        },
        getFilterValuesAsync: function (viewDefinition) {
            return $q(function(resolve, reject) {
                $http.post(appProperties.apiBasePath + '/FilterValues/', viewDefinition).
                    success(function(data, status, headers, config) {
                        resolve(data);
                    })
                    .error(function(data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    });
            });
        },
        postDataAsync:function(d) {
            return $q(function(resolve, reject) {
                $http.post(appProperties.apiBasePath + '/Document/', d).success(function(data, status, headers, config) {
                    resolve(data);
                });
            });
        },
        getDataAsync: function (queryBuilder) {
            return $q(function(resolve, reject) {
                $http.post(appProperties.apiBasePath + '/QueryBuilder/', queryBuilder).
                    success(function(data, status, headers, config) {
                        resolve(data);
                    })
                    .error(function(data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    });
            });
        }
    };
    return result;
}]);