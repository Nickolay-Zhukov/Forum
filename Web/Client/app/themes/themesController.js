'use strict';
angular.module('app.themes', ['ngRoute'])

.config(['$routeProvider', function($routeProvider) {
    $routeProvider.when('/themes', {
        templateUrl: 'client/app/themes/themes.html',
        controller: 'ThemesController',
        controllerAs: 'themes'
    });
}])

.controller('ThemesController', ['$scope', '$rootScope', 'ThemesViewModel', function ($scope, $rootScope, ThemesViewModel) {
    $rootScope.viewTitle = 'Themes -';
    $scope.vm = new ThemesViewModel();
}]);