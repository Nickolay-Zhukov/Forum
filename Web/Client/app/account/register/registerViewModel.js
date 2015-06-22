'use strict';
angular.module('app.account')

.factory('RegisterViewModel', ['$location', 'accountService', function ($location, accountService) {
    return function () {
        var self = this;

        self.user = '';
        self.password = '';
        self.confirmPassword = '';

        self.register = function () {
            accountService.register(self.user, self.password, self.confirmPassword).then(function () {
                $location.path('/login');
            });
        }
    };
}]);