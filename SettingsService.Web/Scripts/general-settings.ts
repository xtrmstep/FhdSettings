/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/models.d.ts" />

class UrlInfo {
    Id: string;
    Url: string;

    constructor(id: string, url: string) {
        this.Id = id;
        this.Url = url;
    }
}

class SettingsServiceApi {
    serviceUrl: string;

    setApiServer(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    loadGeneralSettings() {
        $.get(this.serviceUrl + "/api/hosts/default", (data: HostSettings) => {
            if (data != null) {
                generalSettings.delay(data.CrawlDelay);
                generalSettings.disallow(data.Disallow);
            }
        });
        $.get(this.serviceUrl + "/api/urls", (data: UrlInfo[]) => {
            if (data != null) {
                generalSettings.urls(data);
            }
        });
    }

    saveSettings(disallow: string, delay: number) {
        $.ajax({
            url: this.serviceUrl + "/api/hosts/default",
            method: "PUT",
            data: {
                disallow: disallow,
                delay: delay
            },
            contentType: "application/json"
        });
    }

    removeUrl(id: string) {
        $.ajax({
            url: this.serviceUrl + "/api/urls/" + id,
            method: "DELETE"
        });
    }
}

var generalSettings = {
    delay: ko.observable(60),
    disallow: ko.observable("*"),
    urls: ko.observableArray([]),
    newUrl: ko.observable("http://someurl"),
    saveSettings() {
        var disallow = generalSettings.disallow();
        var delay = generalSettings.delay();
        settingsServiceApi.saveSettings(disallow, delay);
    },
    addUrl() {
        var url = new UrlInfo("", generalSettings.newUrl());
        generalSettings.urls.push(url);
    },
    removeUrl() {
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
