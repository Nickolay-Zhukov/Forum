'use strict';
angular.module('app.account')

.service('localStorageService', ['$window', function ($window) {
    // Private
    var accountDataKey = 'FooForumUser';

    // Public
    this.isAccountDataExist = function () { return !!($window.localStorage.getItem(accountDataKey)); };
    
    this.setAccountData = function (userName, bearerToken) {
        var accountData = {
            userName: userName,
            bearerToken: bearerToken
        }
        $window.localStorage.setItem(accountDataKey, JSON.stringify(accountData));
    }

    this.getUserName = function () { return JSON.parse($window.localStorage.getItem(accountDataKey)).userName; }

    this.getBearerToken = function () { return JSON.parse($window.localStorage.getItem(accountDataKey)).bearerToken; }

    this.removeAccountData = function () { $window.localStorage.removeItem(accountDataKey); }
}])

.service('accountService', ['$http', 'localStorageService', function ($http, localStorageService) {
    var self = this;
    var adminName = 'Admin';

    // Account state
    this.userName = function () {
        return self.isLoggedIn() ? localStorageService.getUserName() : '';
    };

    this.isLoggedIn = localStorageService.isAccountDataExist;

    this.isAdmin = function () {
        return self.userName() == adminName;
    }

    this.isUserLoggedInOrAdmin = function (user) {
        return user == self.userName() || self.isAdmin();
    }

    // Account operations
    this.register = function (username, password, confirmPassword) {
        return $http.post('https://localhost:44300/api/account/register',
        { Username: username, Password: password, ConfirmPassword: confirmPassword });
    };
    this.login = function(username, password) {
        var tokenRequest = {
            method: 'POST',
            url: 'https://localhost:44300/token',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: "userName=" + username + "&password=" + password + "&grant_type=password"
        }
        return $http(tokenRequest).then(function (response) {
            localStorageService.setAccountData(username, response.data.access_token);
        });
    };
    this.logoff = localStorageService.removeAccountData;
}])

.factory('sessionInterceptor', ['localStorageService', function (localStorageService) {
    return {
        request: function (config) {
            if (localStorageService.isAccountDataExist())
                config.headers.Authorization = 'Bearer ' + localStorageService.getBearerToken();
            return config;
        }
    }
}])

.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('sessionInterceptor');
}]);