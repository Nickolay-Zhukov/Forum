'use strict';
angular.module('app', [
  'ngRoute',
  'app.account',
  'app.menubar',
  'app.themes'
])
.config(['$routeProvider', function($routeProvider) {
    $routeProvider.otherwise({ redirectTo: '/themes' });
}]);