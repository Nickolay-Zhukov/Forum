'use strict';
angular.module('app', [
  'ngRoute',
  'mgcrea.ngStrap',
  'app.services',
  'app.uiElements',
  'app.account',
  'app.themes',
  'app.themeDetails'
])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.otherwise({ redirectTo: '/themes' });
}])

.run(['$rootScope', 'accountService', function ($rootScope, accountService) {
    $rootScope.accountState = {
        isAdmin: accountService.isAdmin,

        isAuthorized: accountService.isLoggedIn,

        isItemOwner: function(userName) {
            return accountService.isUserLoggedInOrAdmin(userName);
        }
    }
    $rootScope.alert = {
        message: '',
        isShown: false,
        show: function () { this.isShown = true; },
        hide: function () { this.isShown = false; }
    }
}]);