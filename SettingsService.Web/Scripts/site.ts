/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

class HostsInfo {
    Id: string;
    Host: string;
    Disallow: string;
    CrawlDelay: number;
}

class SettingsServiceApi {
    serviceUrl: string;

    setApiServer(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    loadGeneralSettings() {
        $.get(this.serviceUrl + "/api/hosts/default", (data: HostsInfo) => {
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

    loadHostsSettings() {
        $.get(this.serviceUrl + "/api/hosts", (data: HostsInfo[]) => {
            if (data != null) {
                hostsDetails.hosts(data);
            }
        });
    }

    loadCrawlerRules() {
        $.get(this.serviceUrl + "/api/crawler/rules", (data: HostsInfo[]) => {
            if (data != null) {
                crawlerRules.rules(data);
            }
        });
    }

    saveSettings(hostInfo: HostsInfo, callback: () => any) {
        var jsonValue: any = JSON.stringify(hostInfo);
        $.ajax({
            url: this.serviceUrl + "/api/hosts/" + hostInfo.Id,
            method: "PUT",
            data: jsonValue,
            contentType: "application/json",
            success() {
                callback();
            }
        });
    }

    saveDefaultSettings(disallow: string, delay: number) {
        var jsonValue: any = JSON.stringify({
            disallow: disallow,
            delay: delay
        });
        $.ajax({
            url: this.serviceUrl + "/api/hosts/default",
            method: "PUT",
            data: jsonValue,
            contentType: "application/json"
        });
    }

    addUrl(urlInfo: UrlInfo) {
        var jsonValue: any = JSON.stringify({
            Url: urlInfo.Url
        });
        $.ajax({
            url: this.serviceUrl + "/api/urls",
            method: "POST",
            data: jsonValue,
            contentType: "application/json",
            success: function (data, textStatus: string, jqXHR) {
                urlInfo.Id = data;
            },
            error: function (jqXHR, textStatus: string, errorThrown) {
                alert("Status: " + textStatus + "; Error: " + errorThrown);
            }
        });
    }

    removeUrl(id: string) {
        $.ajax({
            url: this.serviceUrl + "/api/urls/" + id,
            method: "DELETE"
        });
    }

    removeHost(id: string) {
        $.ajax({
            url: this.serviceUrl + "/api/hosts/" + id,
            method: "DELETE"
        });
    }
}
var settingsServiceApi = new SettingsServiceApi();