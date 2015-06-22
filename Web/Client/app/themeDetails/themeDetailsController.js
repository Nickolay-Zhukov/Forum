'use strict';
angular.module('app.themeDetails', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes/:themeId', {
        templateUrl: 'client/app/themeDetails/themeDetails.html',
        controller: 'ThemeDetailsController',
    });
}])

.controller('ThemeDetailsController', ['$scope', '$routeParams', 'ThemeDetailsViewModel', function ($scope, $routeParams, ThemeDetailsViewModel) {
    $scope.vm = new ThemeDetailsViewModel($routeParams.themeId);
}]);