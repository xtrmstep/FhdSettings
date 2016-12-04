/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/site.d.ts" />

class SettingsServiceApi {
    serviceUrl: string;

    setApiServer(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    loadGeneralSettings() {
        $.get(this.serviceUrl + "/api/hosts/default", (data: CrawlHostSetting) => {
            if (data != null) {
                generalSettings.delay(data.crawlDelay);
                generalSettings.disallow(data.disallow);
            }
        });
        $.get(this.serviceUrl + "/api/urls", (data: CrawlUrlSeed[]) => {
            //
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
}

var generalSettings = {
    delay: ko.observable(),
    disallow: ko.observable(),
    saveSettings() {
        settingsServiceApi.saveSettings(this.disallow(), this.delay());
    }
};

var settingsServiceApi = new SettingsServiceApi();
