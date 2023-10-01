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
        GetAllCacheKeys: urlBase + "Cache/GetAllCacheKeys"
    }

    var initialize = function () {

        //return completedPromise;
    }

    function fetchAllCacheKeys() {
        return umbRequestHelper.resourcePromise(
            $http.get(endpoints.GetAllCacheKeys),
            "Failed to retrieve all Person data");
    }

    return {
        initialize,
        fetchAllCacheKeys
    };
};

angular.module('umbraco.resources').factory('fusionCacheToolsResource', fusionCacheToolsResource);