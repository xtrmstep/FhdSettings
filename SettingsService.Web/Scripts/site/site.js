/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
var ServiceApi = (function () {
    function ServiceApi() {
    }
    ServiceApi.prototype.setApiServer = function (baseServiceUrl) {
        this.serviceUrl = baseServiceUrl;
    };
    return ServiceApi;
}());
//# sourceMappingURL=site.js.map