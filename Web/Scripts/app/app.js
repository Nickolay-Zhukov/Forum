'use strict';

angular.module('myApp', [
  'ngRoute',
  'myApp.accountService',
  'myApp.themesService',
  'myApp.Register',
  'myApp.Login',
  'myApp.Themes'
])

.config(['$routeProvider', function($routeProvider) {
    $routeProvider.otherwise({ redirectTo: '/themes' });
}]);