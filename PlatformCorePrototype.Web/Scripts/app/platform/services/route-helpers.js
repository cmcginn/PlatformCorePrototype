﻿myApp.provider('CustomRouteHelpers', ['CUSTOM_APP_REQUIRES', function (customAppRequires) {
    "use strict";

    // Set here the base of the relative path
    // for all app views
    this.basepath = function (uri) {
        return appRoot + uri;
    };

    // Generates a resolve object by passing script names
    // previously configured in constant.APP_REQUIRES
    this.resolveFor = function () {
        var _args = arguments;
        return {
            deps: ['$ocLazyLoad', '$q', function ($ocLL, $q) {
                // Creates a promise chain for each argument
                var promise = $q.when(1); // empty promise
                for (var i = 0, len = _args.length; i < len; i++) {
                    promise = andThen(_args[i]);
                }
                return promise;

                // creates promise to chain dynamically
                function andThen(_arg) {
                    // also support a function that returns a promise
                    if (typeof _arg == 'function')
                        return promise.then(_arg);
                    else
                        return promise.then(function () {
                            // if is a module, pass the name. If not, pass the array
                            var whatToLoad = getRequired(_arg);
                            // simple error check
                            if (!whatToLoad) return $.error('Route resolve: Bad resource name [' + _arg + ']');
                            // finally, return a promise
                            return $ocLL.load(whatToLoad);
                        });
                }
                // check and returns required data
                // analyze module items with the form [name: '', files: []]
                // and also simple array of script files (for not angular js)
                function getRequired(name) {
                    if (customAppRequires.modules)
                        for (var m in customAppRequires.modules)
                            if (customAppRequires.modules[m].name && customAppRequires.modules[m].name === name)
                                return customAppRequires.modules[m];
                    return customAppRequires.scripts && customAppRequires.scripts[name];
                }

            }]
        };
    }; // resolveFor

    // not necessary, only used in config block for routes
    this.$get = function () {
        return {
            basepath: this.basepath
        }
    };

}]);