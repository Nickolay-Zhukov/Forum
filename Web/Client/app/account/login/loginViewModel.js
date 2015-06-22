'use strict';
angular.module('app.account')

.factory('LoginViewModel', ['$location', 'accountService', function ($location, accountService) {
    return function () {
        var self = this;

        self.user = '';
        self.password = '';

        self.login = function () {
            accountService.login(self.user, self.password).then(function () {
                $location.path('/themes');
            });
        }
    };
}]);