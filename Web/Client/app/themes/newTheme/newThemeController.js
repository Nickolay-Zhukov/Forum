'use strict';
angular.module('app.themes')

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes/create-new-theme', {
        templateUrl: 'client/app/themes/newTheme/newTheme.html',
        controller: 'NewThemeController',
    });
}])

.controller('NewThemeController', ['$scope', 'NewThemeViewModel', function ($scope, NewThemeViewModel) {
    $scope.vm = new NewThemeViewModel();
}]);