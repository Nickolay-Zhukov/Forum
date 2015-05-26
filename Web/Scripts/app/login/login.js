'use strict';

angular.module('myApp.Login', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/login', {
        templateUrl: 'scripts/app/login/login.html',
        controller: 'LoginController'
    });
}])

.controller('LoginController', ['$scope', 'accountService', function ($scope, accountService) {
    $scope.user = '';
    $scope.password = '';

    $scope.login = function () {
        accountService.login($scope.user, $scope.password).then(
            function (data) { },
            function (reason) { });
    };
}]);