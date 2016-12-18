/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/models.d.ts" />
var UrlInfo = (function () {
    function UrlInfo(id, url) {
        this.Id = id;
        this.Url = url;
    }
    return UrlInfo;
}());
var SettingsServiceApi = (function () {
    function SettingsServiceApi() {
    }
    SettingsServiceApi.prototype.setApiServer = function (baseServiceUrl) {
        this.serviceUrl = baseServiceUrl;
    };
    SettingsServiceApi.prototype.loadGeneralSettings = function () {
        $.get(this.serviceUrl + "/api/hosts/default", function (data) {
            if (data != null) {
                generalSettings.delay(data.CrawlDelay);
                generalSettings.disallow(data.Disallow);
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
    SettingsServiceApi.prototype.removeUrl = function (id) {
        $.ajax({
            url: this.serviceUrl + "/api/urls/" + id,
            method: "DELETE"
        });
    };
    return SettingsServiceApi;
}());
var generalSettings = {
    delay: ko.observable(60),
    disallow: ko.observable("*"),
    urls: ko.observableArray([]),
    newUrl: ko.observable("http://someurl"),
    saveSettings: function () {
        var disallow = generalSettings.disallow();
        var delay = generalSettings.delay();
        settingsServiceApi.saveSettings(disallow, delay);
    },
    addUrl: function () {
        var url = new UrlInfo("", generalSettings.newUrl());
        generalSettings.urls.push(url);
    },
    removeUrl: function () {
        var urls = generalSettings.urls();
        for (var i = 0; i < urls.length; i++) {
            if (urls[i].Id === this.Id) {
                urls.splice(i, 1); // remove item from the array
                generalSettings.urls(urls);
                settingsServiceApi.removeUrl(urls[i].Id);
                break;
            }
        }
    }
};
var settingsServiceApi = new SettingsServiceApi();
//# sourceMappingURL=general-settings.js.map