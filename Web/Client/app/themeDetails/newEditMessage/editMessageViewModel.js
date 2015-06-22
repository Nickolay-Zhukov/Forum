'use strict';
angular.module('app.themeDetails')

.factory('EditMessageViewModel', ['$rootScope', '$location', 'serverApiService', function ($rootScope, $location, serverApiService) {
    return function (themeId, id) {
        var operationName = 'Edit message';
        $rootScope.viewTitle = operationName + ' -';

        var self = this;
        self.pageTitle = operationName;
        self.themeId = themeId;
        serverApiService.getMessageById(themeId, id).success(function (data) {
            self.text = data.Text;
        });

        self.saveMessage = function () {
            if (self.text) {
                serverApiService.editMessage(themeId, id, self.text).success(function () {
                    $location.path('/themes/' + themeId);
                });
            }
        }
    };
}]);