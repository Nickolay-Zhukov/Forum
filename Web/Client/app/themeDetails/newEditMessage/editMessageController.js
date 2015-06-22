'use strict';
angular.module('app.themeDetails')

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes/:themeId/edit-message/:messageId', {
        templateUrl: 'client/app/themeDetails/newEditMessage/newEditMessage.html',
        controller: 'EditMessageController',
    });
}])

.controller('EditMessageController', ['$scope', '$routeParams', 'EditMessageViewModel', function ($scope, $routeParams, EditMessageViewModel) {
    $scope.vm = new EditMessageViewModel($routeParams.themeId, $routeParams.messageId);
}]);