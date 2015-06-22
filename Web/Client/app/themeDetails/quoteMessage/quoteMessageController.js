'use strict';
angular.module('app.themeDetails')

.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/themes/:themeId/quote-message/:messageId', {
        templateUrl: 'client/app/themeDetails/quoteMessage/quoteMessage.html',
        controller: 'QuoteMessageController',
    });
}])

.controller('QuoteMessageController', ['$scope', '$routeParams', 'QuoteMessageViewModel', function ($scope, $routeParams, QuoteMessageViewModel) {
    $scope.vm = new QuoteMessageViewModel($routeParams.themeId, $routeParams.messageId);
}]);