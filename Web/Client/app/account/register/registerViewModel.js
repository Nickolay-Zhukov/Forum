'use strict';
angular.module('app.account')

.factory('RegisterViewModel', ['accountService', function (accountService) {
    return function () {
        var self = this;

        self.user = '';
        self.password = '';
        self.confirmPassword = '';

        self.register = function () { accountService.register(self.user, self.password, self.confirmPassword); }
    };
}]);