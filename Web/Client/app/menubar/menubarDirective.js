'use strict';
angular.module('app.menubar', [])

.directive('menubar', ['accountService', function (accountService) {
    return {
        templateUrl: 'client/app/menubar/menubar.html',
        scope: {},
        link: function (scope) {
            scope.$watch(accountService.isLoggedIn, function (value) {
                scope.isLoggedIn = value;
            });
            scope.$watch(function () { return accountService.userName; }, function (value) {
                scope.userName = value;
            });
        }
    };
}]);