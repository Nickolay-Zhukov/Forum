'use strict';
angular.module('app.themes')

.factory('ThemesViewModel', ['$rootScope', '$location', 'serverApiService', 'accountService', 'uiElementsService', function ($rootScope, $location, serverApiService, accountService, uiElementsService) {
    return function () {
        $rootScope.viewTitle = 'Themes -';
        var self = this;

        // Get all themes
        function getThemes () {
            serverApiService.getAllThemes().success(function (data) { self.themes = data; });
        };
        getThemes ();

        // Create new theme
        self.createNewTheme = function () {
            var redirectPath = accountService.isLoggedIn()
                ? '/themes/create-new-theme'
                : '/login';
            $location.path(redirectPath);
        }

        // Delete theme
        var modalContent = {
            title: 'Theme delete confirmation',
            message: 'Are you sure you want to delete this theme?'
        }
        function deleteTheme(id) {
            serverApiService.deleteTheme(id).finally(getThemes);
        }
        self.deleteTheme = function (themeId) {
            uiElementsService.deleteConfirmation(modalContent, function () {
                deleteTheme(themeId);
            });
        }
    };
}]);