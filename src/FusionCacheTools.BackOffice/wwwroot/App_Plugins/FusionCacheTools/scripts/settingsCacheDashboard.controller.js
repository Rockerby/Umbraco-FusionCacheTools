(function () {
    'use strict';

    function settingsCacheDashboardController($scope, $q, $controller, fusionCacheToolsResource) {

        var vm = this;

        vm.versionInfo = {
            IsCurrent: true
        };
        vm.cacheKeys = [];

        $scope.vm = vm;

        init();

        function init() {
            fusionCacheToolsResource.fetchAllCacheKeys().then(function (response) {
                console.log("Recording data", response);
                vm.cacheKeys = response;
            });
        }
    }

    angular.module('umbraco')
        .controller('settingsCacheDashboardController', settingsCacheDashboardController);
})();




