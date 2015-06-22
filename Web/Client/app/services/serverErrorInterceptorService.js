'use strict';
angular.module('app.services')

.factory('serverErrorInterceptor', ['$q', '$rootScope', function ($q, $rootScope) {
    return {
        'responseError': function (response) {
            var errorMessage = 'Server error';
            if (response.headers()['content-type'] == 'text/plain; charset=utf-8') {
                errorMessage = response.data;
            }
            if (response.data.Message) {
                errorMessage = response.data.Message;
            }
            if (response.data.error_description) {
                errorMessage = response.data.error_description;
            }
            $rootScope.alert.message = errorMessage;
            $rootScope.alert.show();
            return $q.reject(response);
        }
    };
}])

.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('serverErrorInterceptor');
}]);