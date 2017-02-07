/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming
class UrlInfo {
    Id: string;
    Url: string;

    constructor(url: string) {
        this.Url = url;
    }
}
// ReSharper restore InconsistentNaming

var generalSettings = {
    delay: ko.observable(null),
    disallow: ko.observable(""),
    urls: ko.observableArray([]),
    newUrl: ko.observable(""),
    saveSettings() {
        var disallow = generalSettings.disallow();
        var delay = generalSettings.delay();
        // saving default
        settingsServiceApi.saveDefaultSettings(disallow, delay);
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
        settingsServiceApi.addUrl(url);
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
                    settingsServiceApi.removeUrl(removed.Id);
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
                settingsServiceApi.saveSettings(
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
                    settingsServiceApi.removeHost(removed.Id);
                    break;
                }
            }
        }
    }
};
