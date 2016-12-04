/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/site.d.ts" />
var SettingsServiceApi = (function () {
    function SettingsServiceApi() {
    }
    SettingsServiceApi.prototype.setApiServer = function (baseServiceUrl) {
        this.serviceUrl = baseServiceUrl;
    };
    SettingsServiceApi.prototype.loadGeneralSettings = function () {
        $.get(this.serviceUrl + "/api/hosts/default", function (data) {
            if (data != null) {
                generalSettings.delay(data.crawlDelay);
                generalSettings.disallow(data.disallow);
            }
        });
        $.get(this.serviceUrl + "/api/urls", function (data) {
            if (data != null) {
                generalSettings.urls(data);
            }
        });
    };
    SettingsServiceApi.prototype.saveSettings = function (disallow, delay) {
        $.ajax({
            url: this.serviceUrl + "/api/hosts/default",
            method: "PUT",
            data: {
                disallow: disallow,
                delay: delay
            },
            contentType: "application/json"
        });
    };
    return SettingsServiceApi;
}());
var generalSettings = {
    delay: ko.observable(),
    disallow: ko.observable(),
    urls: ko.observableArray(),
    saveSettings: function () {
        settingsServiceApi.saveSettings(this.disallow(), this.delay());
    },
    addUrl: function () {
    }
};
var settingsServiceApi = new SettingsServiceApi();
