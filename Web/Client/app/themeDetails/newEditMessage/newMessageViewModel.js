'use strict';
angular.module('app.themeDetails')

.factory('NewMessageViewModel', ['$rootScope', '$location', 'serverApiService', function ($rootScope, $location, serverApiService) {
    return function (themeId) {
        var operationName = 'Add new message';
        $rootScope.viewTitle = operationName + ' -';

        var self = this;
        self.pageTitle = operationName;
        self.themeId = themeId;
        self.text = '';

        self.saveMessage = function () {
            serverApiService.addNewMessage(themeId, self.text).success(function () { $location.path('/themes/' + themeId); });
        }
    };
}]);