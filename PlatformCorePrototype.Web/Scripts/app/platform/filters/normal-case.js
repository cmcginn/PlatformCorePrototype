myApp.filter('normalCase', [
    function() {

        function toNormalCase(value) {
            var result = value.replace(/_/g, '');
            result = result.replace(/([A-Z])/g, ' $1');
            result = result.replace(/^./, function(str) { return str.toUpperCase(); });
            return result;
        }
        return function (value) {
            return toNormalCase(value);
        };
    }]);