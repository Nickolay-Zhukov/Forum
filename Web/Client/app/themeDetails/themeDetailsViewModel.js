'use strict';
angular.module('app.themeDetails')

.factory('ThemeDetailsViewModel', ['$rootScope', '$location', 'serverApiService', 'accountService', 'uiElementsService', function ($rootScope, $location, serverApiService, accountService, uiElementsService) {
    return function (themeId) {
        var self = this;

        // Get all messages
        function getMessagesList () {
            serverApiService.getThemeById(themeId).success(function (data) {
                self.theme = data;
                self.messages = data.Messages;
                $rootScope.viewTitle = self.theme.Title + ' -';
            });
        };
        getMessagesList();

        // Add message
        self.addMessage = function () {
            var redirectPath = accountService.isLoggedIn()
                ? '/themes/' + themeId + '/new-message'
                : '/login';
            $location.path(redirectPath);
        }

        // Delete message
        var modalContent = {
            title: 'Message delete confirmation',
            message: 'Are you sure you want to delete this message?'
        }
        function deleteMessage(routeIds) {
            serverApiService.deleteMessage(routeIds.themeId, routeIds.messageId).finally(getMessagesList);
        }
        self.deleteMessage = function (messageId) {
            var routeIds = { themeId: themeId, messageId: messageId }
            uiElementsService.deleteConfirmation(modalContent, function () {
                deleteMessage(routeIds);
            });
        }
    };
}]);