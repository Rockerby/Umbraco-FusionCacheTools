function fusionCacheToolsResource($rootScope, $q, $http, umbRequestHelper) {

    if (typeof (Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath) == 'undefined') {
        console.log("boev :: path isn't set yet");
        //return {};
    }

    var proxy = null;
    const sessionCookieId = '_boev_sessionid';
    let sessionId = '';

    let urlBase = 'backoffice/FusionCacheTools/';

    let endpoints = {
        GetAllCacheKeys: urlBase + "Cache/GetAllCacheKeys",
        GetCacheItem: urlBase + "Cache/GetCacheItem",
        RemoveCacheItem: urlBase + "Cache/RemoveCacheItem"
    }

    var initialize = function () {

        //return completedPromise;
    }

    function fetchAllCacheKeys() {
        return umbRequestHelper.resourcePromise(
            $http.get(endpoints.GetAllCacheKeys),
            "Failed to retrieve all Person data");
    }

    /*
    *
    */
    function fetchItem(cacheKey) {

        let url = endpoints.GetCacheItem + '?' +
            new URLSearchParams({
                key: cacheKey
            });

        return umbRequestHelper.resourcePromise(
            $http.get(url),
            "Failed to retrieve all Person data");
    }

    function removeCache(cacheKey) {

        let url = endpoints.RemoveCacheItem + '?' +
            new URLSearchParams({
                key: cacheKey
            });

        return umbRequestHelper.resourcePromise(
            $http.get(url),
            "Failed to retrieve all Person data");
    }

    return {
        initialize,
        fetchAllCacheKeys,
        fetchItem,
        removeCache
    };
};

angular.module('umbraco.resources').factory('fusionCacheToolsResource', fusionCacheToolsResource);