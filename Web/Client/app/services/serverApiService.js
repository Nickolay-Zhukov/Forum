'use strict';
angular.module('app.services', [])

.service('serverApiService', ['$http', function ($http) {
    // Themes operations
    this.getAllThemes = function () {
        return $http.get('https://localhost:44300/api/themes');
    }

    this.getThemeById = function (id) {
        return $http.get('https://localhost:44300/api/themes/' + id);
    }

    this.createNewTheme = function (themeTitle) {
        return $http.post('https://localhost:44300/api/themes', { Title: themeTitle });
    }

    this.deleteTheme = function (id) {
        return $http.delete('https://localhost:44300/api/themes/' + id);
    }

    // Messages operation
    this.getMessageById = function (themeId, id) {
        return $http.get('https://localhost:44300/api/themes/' + themeId + '/message/' + id);
    }

    this.addNewMessage = function (themeId, messageText) {
        return $http.post('https://localhost:44300/api/themes/' + themeId + '/message', { Text: messageText });
    }

    this.editMessage = function(themeId, id, messageText) {
        return $http.put('https://localhost:44300/api/themes/' + themeId + '/message/' + id, { Text: messageText });
    }

    this.deleteMessage = function (themeId, id) {
        return $http.delete('https://localhost:44300/api/themes/' + themeId + '/message/' + id);
    }

    this.quoteMessage = function (themeId, id, messageText) {
        return $http.post('https://localhost:44300/api/themes/' + themeId + '/message/' + id, { Text: messageText });
    }
}]);