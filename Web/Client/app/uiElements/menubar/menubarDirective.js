'use strict';
angular.module('app.uiElements')

.directive('menubar', ['accountService', function (accountService) {
    return {
        scope: {},
        templateUrl: 'client/app/uiElements/menubar/menubar.html',
        link: function (scope) {
            scope.$watch(accountService.isLoggedIn, function(value) {
                scope.isLoggedIn = value;
            });
            scope.$watch(accountService.userName, function (value) {
                scope.userName = value;
            });
            scope.logoff = accountService.logoff;
        }
    };
}]);