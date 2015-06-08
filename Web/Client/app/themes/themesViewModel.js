'use strict';
angular.module('app.themes')

.factory('ThemesViewModel', ['themesService', function(themesService) {
    return function() {
        var self = this;
        themesService.getThemes().then(function(response) { self.themes = response.data; });
    };
}]);