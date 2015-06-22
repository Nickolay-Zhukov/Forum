'use strict';
angular.module('app.themes', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes', {
        templateUrl: 'client/app/themes/themes.html',
        controller: 'ThemesController',
    });
}])

.controller('ThemesController', ['$scope', 'ThemesViewModel', function ($scope, ThemesViewModel) {
    $scope.vm = new ThemesViewModel();
}]);