'use strict';
angular.module('app.themeDetails')

.factory('QuoteMessageViewModel', ['$rootScope', '$location', 'serverApiService', function ($rootScope, $location, serverApiService) {
    return function (themeId, id) {
        $rootScope.viewTitle = 'Add new message with quote -';

        var self = this;
        self.text = '';
        self.themeId = themeId;
        serverApiService.getMessageById(themeId, id).success(function (data) {
            self.quoteAuthor = data.Author;
            self.quoteText = data.Text;
        });

        self.saveMessage = function () {
            if (self.quoteText) {
                serverApiService.quoteMessage(themeId, id, self.text).success(function () {
                    $location.path('/themes/' + themeId);
                });
            }
        }
    };
}]);