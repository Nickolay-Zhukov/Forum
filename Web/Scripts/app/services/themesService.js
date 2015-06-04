'use strict';

angular.module('myApp.themesService', [])

.service('themesService', ['$http', function ($http) {
	return {
	    getThemes: function () {
	        return $http.get('https://localhost:44300/api/Themes', {});
	    }
	};
}]);