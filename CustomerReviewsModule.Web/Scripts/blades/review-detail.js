angular.module('customerReviewsModule')
    .controller('customerReviewsModule.reviewDetailController', ['$scope', 'customerReviewsModule.webApi', 'platformWebApp.bladeUtils', 'uiGridConstants',
        function ($scope, reviewsApi, bladeUtils, uiGridConstants) {
            $scope.uiGridConstants = uiGridConstants;

            var blade = $scope.blade;


            function initializeBlade(data) {

                blade.currentEntityId = data.id;
                blade.title = data.name;

                blade.currentEntity = angular.copy(data);
                blade.origEntity = data;
                blade.isLoading = false;
            }

            $scope.setForm = function (form) { $scope.formScope = form; };


            blade.headIcon = 'fa-archive';

            initializeBlade(angular.copy(blade.currentEntity));
        }]);