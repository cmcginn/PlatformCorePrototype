myApp.config([
    '$stateProvider', '$locationProvider', '$urlRouterProvider', 'CustomRouteHelpersProvider',
    function ($stateProvider, $locationProvider, $urlRouterProvider, helper) {
        $urlRouterProvider.otherwise('/');
        $stateProvider
            .state('platform', {
                //url: '/',
                abstract: true,
                controller: 'AppController',
                resolve: helper.resolveFor('icons')
            })
            .state('platform.home', {
                url: '/',
                title: 'Home',
                templateUrl: helper.basepath('Home/Index'),
                resolve: helper.resolveFor('isteven-multi-select', 'whirl', 'ngDialog')
            }).state('platform.scratch', {
                url: '/Scratch',
                templateUrl: helper.basepath('Home/Scratch'),
                resolve: helper.resolveFor('angularBootstrapNavTree'),
                title: 'Home'
            });
    }
]);