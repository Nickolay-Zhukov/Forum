'use strict';
angular.module('app.account')

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/register', {
            templateUrl: 'client/app/account/register/register.html',
            controller: 'RegisterController',
        });
}])

.controller('RegisterController', ['$scope', '$rootScope', 'RegisterViewModel', function ($scope, $rootScope, RegisterViewModel) {
    $rootScope.viewTitle = 'Register -';
    $scope.vm = new RegisterViewModel();
}]);