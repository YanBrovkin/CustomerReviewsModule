angular.module('customerReviewsModule')
    .controller('customerReviewsModule.reviewDetailController', ['$scope', 'customerReviewsModule.webApi', 'platformWebApp.bladeUtils', 'uiGridConstants',
        function ($scope, reviewsApi, bladeUtils, uiGridConstants) {
            $scope.uiGridConstants = uiGridConstants;

            var blade = $scope.blade;


            function initializeBlade(data) {

                blade.currentEntityId = data.id;
                blade.title = 'customerReviewsModule.blades.review-detail.labels.title';

                blade.currentEntity = angular.copy(data);
                blade.origEntity = data;
                blade.isLoading = false;
            }

            $scope.setForm = function (form) { $scope.formScope = form; };
            var maxRate = 5;
            $scope.getRepeater = function () {
                return new Array(maxRate);
            };

            blade.headIcon = 'fa-archive';

            initializeBlade(blade.currentEntity);
        }]);