'use strict';

angular.module('myApp.Themes', ['ngRoute'])

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes', {
        templateUrl: 'scripts/app/themes/themes.html',
        controller: 'themesController'
    });
}])

.controller('themesController', ['$scope', '$rootScope', 'ThemesVm', function ($scope, $rootScope, ThemesVm) {
    $rootScope.viewTitle = "Themes";
    $scope.vm = new ThemesVm();
}]);