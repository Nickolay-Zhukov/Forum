'use strict';

angular.module('myApp.Login', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/login', {
        templateUrl: 'scripts/app/login/login.html',
        controller: 'LoginController'
    });
}])

.controller('LoginController', ['$scope', '$rootScope', '$location', 'accountService', function ($scope, $rootScope, $location, accountService) {
    $rootScope.viewTitle = "Log in";

    $scope.user = '';
    $scope.password = '';

    $scope.login = function () {
        accountService.login($scope.user, $scope.password).then(function() {
            $rootScope.isLoggedIn = true;
            $rootScope.userName = $scope.user;

            //scope.$apply(function () { $location.path("/themes"); });

            $location.path("/themes");
            $scope.$apply();
        });
    };

    $rootScope.logoff = function() {
        accountService.logoff();
        $rootScope.isLoggedIn = false;
    };
}]);