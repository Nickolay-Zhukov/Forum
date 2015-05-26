'use strict';

angular.module('myApp.Register', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/register', {
        templateUrl: 'scripts/app/register/register.html',
        controller: 'RegisterController'
  });
}])

.controller('RegisterController', ['$scope', 'accountService', function ($scope, accountService) {
    $scope.user = '';
    $scope.password = '';
    $scope.confirmPassword = '';

	$scope.register = function () {
		debugger;
		accountService.register($scope.user, $scope.password, $scope.confirmPassword).then(
            function (data) { var order = data; },
            function (reason) { }
        );
	};

	$scope.token = function () {
		debugger;
		accountService.token($scope.user, $scope.password).then(
            function (data) { var order = data; },
            function (reason) { }
        );
	};
}]);