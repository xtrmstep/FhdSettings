/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming

class HostsInfo {
    Id: string;
    Host: string;
    Disallow: string;
    CrawlDelay: number;
}

class UrlInfo {
    Id: string;
    Url: string;

    constructor(url: string) {
        this.Url = url;
    }
}

// ReSharper restore InconsistentNaming

class GeneralSettingsApi extends ServiceApi {

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
var generalSettingsApi = new GeneralSettingsApi();

var generalSettings = {
    delay: ko.observable(null),
    disallow: ko.observable(""),
    urls: ko.observableArray([]),
    newUrl: ko.observable(""),
    saveSettings() {
        var disallow = generalSettings.disallow();
        var delay = generalSettings.delay();
        // saving default
        generalSettingsApi.saveDefaultSettings(disallow, delay);
    },
    addUrl() {
        var newUrl = generalSettings.newUrl();
        if (newUrl.trim() === "") {
            alert("URL is required.");
            return;
        }
        // new URL will have ID after the call to the server, in callback
        var url = new UrlInfo(newUrl);
        generalSettings.urls.push(url);
        generalSettingsApi.addUrl(url);
        generalSettings.newUrl(""); // clean the input
    },
    removeUrl() {
        if (confirm("Are you sure?")) {
            var urls = generalSettings.urls();
            for (var i = 0; i < urls.length; i++) {
                var removed: UrlInfo = urls[i];
                if (removed.Id === this.Id) {
                    urls.splice(i, 1); // remove item from the grid array 
                    generalSettings.urls(urls);
                    generalSettingsApi.removeUrl(removed.Id);
                    break;
                }
            }
        }
    }
};

var hostsDetails = {
    hosts: ko.observableArray([]),
    // currently editing values
    id: ko.observable(""),
    host: ko.observable(""),
    delay: ko.observable(null),
    disallow: ko.observable(""),

    saveHost() {
        var updatedHost = new HostsInfo();
        updatedHost.Id = hostsDetails.id();
        updatedHost.Host = hostsDetails.host();
        updatedHost.CrawlDelay = hostsDetails.delay();
        updatedHost.Disallow = hostsDetails.disallow();

        var hosts = hostsDetails.hosts();
        for (var i = 0; i < hosts.length; i++) {
            var item: HostsInfo = hosts[i];
            if (item.Id === updatedHost.Id) {
                generalSettingsApi.saveSettings(
                    updatedHost,
                    () => {
                        // refresh item of the grid
                        hostsDetails.hosts.replace(hosts[i], updatedHost);
                        hostsDetails.eventHostSaved();
                    }
                );
                break;
            }
        }
    },

    eventHostSaved() { },

    removeHost() {
        if (confirm("Are you sure?")) {
            var hosts = hostsDetails.hosts();
            for (var i = 0; i < hosts.length; i++) {
                var removed: HostsInfo = hosts[i];
                if (removed.Id === this.Id) {
                    hosts.splice(i, 1); // remove item from the grid array 
                    hostsDetails.hosts(hosts);
                    generalSettingsApi.removeHost(removed.Id);
                    break;
                }
            }
        }
    }
};
