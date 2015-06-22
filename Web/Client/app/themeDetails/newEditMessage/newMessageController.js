'use strict';
angular.module('app.themeDetails')

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes/:themeId/new-message', {
        templateUrl: 'client/app/themeDetails/newEditMessage/newEditMessage.html',
        controller: 'NewMessageController',
    });
}])

.controller('NewMessageController', ['$scope', '$routeParams', 'NewMessageViewModel', function ($scope, $routeParams, NewMessageViewModel) {
    $scope.vm = new NewMessageViewModel($routeParams.themeId);
}]);