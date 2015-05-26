'use strict';

angular.module('myApp.Themes', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes', {
        templateUrl: 'scripts/app/themes/themes.html',
        controller: 'ThemesController'
    });
}])

.controller('ThemesController', ['$scope', 'themesService', function ($scope, themesService) {
    themesService.getThemes().then(function (data) {
        $scope.themes = data.data;
    });
}]);