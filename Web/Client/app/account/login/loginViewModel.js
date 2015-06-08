'use strict';
angular.module('app.account')

.factory('LoginViewModel', ['accountService', function (accountService) {
    return function () {
        var self = this;

        self.user = '';
        self.password = '';

        self.login = function () { accountService.login(self.user, self.password); }
    };
}]);