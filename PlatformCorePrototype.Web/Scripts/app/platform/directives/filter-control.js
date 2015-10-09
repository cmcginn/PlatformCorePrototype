myApp.directive('filterControl', ['CUSTOM_APP_PROPERTIES', function (constants) {


    function link(scope, element, attrs) {

        // console.log(scope);
        element.addClass('filter-control');
        element.attr('id', scope.filterId);
        function init() {
            scope.visible = true;

        }

        scope.$watch(scope.filterValues);
        scope.onFilterClose = function () {
            if (scope.filterValues.length > 0) {
                element.removeClass('invalid');
                element.find('.filter-error-message').text('');
            }

            scope.$emit('filterSelectionChanged', scope);

        }
        scope.onFilterOpen = function () {

        }
        scope.$on('renderFilter', function (e, a) {

            //key value format
            if (a.filterId == scope.filterId) {

                init();
            }

        });
        scope.$on('filterInvalid', function (e, a) {
            if (a.filterId == scope.filterId) {
                element.addClass('invalid');
                element.find('.filter-error-message').text('Selection Required');
            }
        });
        scope.$on('clearFilters', function (e, a) {
            angular.forEach(scope.filterValues, function (val) {

                val.active = val.value == scope.filterDefaultValue;

            });
            scope.filterSelection = [];
        });
    }
    var result = {
        link: link,
        templateUrl: constants.templateBasePath + '/filter-control.html',
        //require: '^istevenMultiSelect',
        scope: {
            filterId: '=',
            filterDisplayName: '=',
            filterValues: '=',
            filterSelectionMode: '=',
            filterDefaultValue: '='

        }

    }
    return result;
}]);