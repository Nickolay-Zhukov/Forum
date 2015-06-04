'use strict';

angular.module('myApp.accountService', [])

.factory('sessionInterceptor', ['sessionService', function (sessionService) {
    var sessionInterceptor = {
        request: function (config) {
            if (sessionService.isLoggedIn()) {
                config.headers.authorization = 'bearer ' + sessionService.getToken();
            }
            return config;
        }
    };
    return sessionInterceptor;
}])

.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('sessionInterceptor');
}])

.service('sessionService', [function() {
    var session = {};
    return {
        setToken: function (token) {
            session.token = token;
        },
        getToken: function() {
            return session.token;
        },
        clearSession: function() {
            session.token = undefined;
        },
        isLoggedIn: function() {
            return !!session.token;
        }
    };
}])

.service('accountService', ['$http', 'sessionService', function ($http, sessionService) {
	return {
		register: function (username, password, confirmPassword) {
			return $http.post('https://localhost:44300/api/account/register',
				{ Username: username, Password: password, ConfirmPassword: confirmPassword });
		},
		login: function (username, password) {
			var tokenRequest = {
			    method: 'POST',
			    url: 'https://localhost:44300/token',
			    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
			    data: "userName=" + username + "&password=" + password + "&grant_type=password"
            }
			return $http(tokenRequest).then(function (response) {
			    sessionService.setToken(response.data.access_token);
			});
		},
        logoff: function() {
            sessionService.clearSession();
        }
	};
}]);