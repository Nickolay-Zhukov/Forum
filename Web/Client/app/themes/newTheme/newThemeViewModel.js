'use strict';
angular.module('app.themes')

.factory('NewThemeViewModel', ['$rootScope', '$location', 'serverApiService', function ($rootScope, $location, serverApiService) {
    return function () {
        $rootScope.viewTitle = 'Create new theme -';
        var self = this;

        self.themeTitle = '';
        self.createNewTheme = function () {
            serverApiService.createNewTheme(self.themeTitle).success(function () { $location.path('/theme'); });
        }
    };
}]);