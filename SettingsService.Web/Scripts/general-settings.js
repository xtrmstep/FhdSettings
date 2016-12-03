/// <reference path="typings/jquery/jquery.d.ts" />
var CrawlHostSetting = (function () {
    function CrawlHostSetting() {
    }
    return CrawlHostSetting;
}());
var SettingsServiceApi = (function () {
    function SettingsServiceApi(baseServiceUrl) {
        this.serviceUrl = baseServiceUrl;
    }
    SettingsServiceApi.prototype.loadGeneralSettings = function (buildDefaultSettings, buildFrontierSettings) {
        $.get(this.serviceUrl + "/api/hosts?host=''", function (data) { buildDefaultSettings(data.disallow, data.crawlDelay); });
        $.get(this.serviceUrl + "/api/urls", function (data) { buildFrontierSettings(data); });
    };
    return SettingsServiceApi;
}());
//# sourceMappingURL=general-settings.js.map