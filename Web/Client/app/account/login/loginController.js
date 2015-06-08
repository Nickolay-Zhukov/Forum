'use strict';
angular.module('app.account', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
    $routeProvider
        .when('/login', {
            templateUrl: 'client/app/account/login/login.html',
            controller: 'LoginController',
        });
}])

.controller('LoginController', ['$scope', '$rootScope', 'LoginViewModel', function($scope, $rootScope, LoginViewModel) {
    $rootScope.viewTitle = 'Login -';
    $scope.vm = new LoginViewModel();
}]);