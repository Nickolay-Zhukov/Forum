'use strict';
angular.module('app.account')

.factory('sessionInterceptor', ['sessionService', function (sessionService) {
    return {
        request: function (config) {
            if (sessionService.isLoggedIn()) config.headers.Authorization = 'Bearer ' + sessionService.getToken();
            return config;
        }
    }
}])

.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('sessionInterceptor');
}])

.service('sessionService', ['$window', function ($window) {
    return {
        setToken: function (token) {
            $window.localStorage.setItem('BearerToken', token);
        },
        getToken: function () {
            return $window.localStorage.getItem('BearerToken');
        },
        clearSession: function () {
            $window.localStorage.setItem('BearerToken', undefined);
        },
        isLoggedIn: function () {
            return !!($window.localStorage.getItem('BearerToken'));
        }
    };
}])

.service('accountService', ['$http', 'sessionService', function ($http, sessionService) {

    return {
        isLoggedIn: sessionService.isLoggedIn,
        userName: '',

        register: function (username, password, confirmPassword) {
            return $http.post('https://localhost:44300/api/account/register',
            { Username: username, Password: password, ConfirmPassword: confirmPassword });
        },

        login: function (username, password) {

            var self = this;
            var tokenRequest =
                {
                    method: 'POST',
                    url: 'https://localhost:44300/token',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    data: "userName=" + username + "&password=" + password + "&grant_type=password"
                }
            return $http(tokenRequest).then(function (response) {
                sessionService.setToken(response.data.access_token);
                self.userName = username;
            });
        },

        logoff: function () {
            sessionService.clearSession();
            this.userName = '';
        }
    };
}]);