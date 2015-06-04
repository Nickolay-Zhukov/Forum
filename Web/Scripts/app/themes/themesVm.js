'use strict';

angular.module('myApp.Themes')

.factory('ThemesVm', ['themesService', function (themesService) {
    return function() {
        var self = this;

        themesService.getThemes().then(function (response) {
            self.themes = response.data;
        });
    };
}]);