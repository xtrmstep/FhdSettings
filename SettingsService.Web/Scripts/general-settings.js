/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/models.d.ts" />
var UrlInfo = (function () {
    function UrlInfo(url) {
        this.Url = url;
    }
    return UrlInfo;
}());
var HostsInfo = (function () {
    function HostsInfo(host, disallow, delay) {
        this.Host = host;
        this.Disallow = disallow;
        this.CrawlDelay = delay;
    }
    return HostsInfo;
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
    SettingsServiceApi.prototype.loadHostsSettings = function () {
        $.get(this.serviceUrl + "/api/hosts", function (data) {
            if (data != null) {
                hostsDetails.hosts(data);
            }
        });
    };
    SettingsServiceApi.prototype.saveSettings = function (disallow, delay) {
        $.ajax({
            url: this.serviceUrl + "/api/hosts/default",
            method: "PUT",
            data: JSON.stringify({
                disallow: disallow,
                delay: delay
            }),
            contentType: "application/json"
        });
    };
    SettingsServiceApi.prototype.addUrl = function (urlInfo) {
        $.ajax({
            url: this.serviceUrl + "/api/urls",
            method: "POST",
            data: JSON.stringify({
                Url: urlInfo.Url
            }),
            contentType: "application/json",
            success: function (data, textStatus, jqXHR) {
                urlInfo.Id = data;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Status: " + textStatus + "; Error: " + errorThrown);
            }
        });
    };
    SettingsServiceApi.prototype.removeUrl = function (id) {
        $.ajax({
            url: this.serviceUrl + "/api/urls/" + id,
            method: "DELETE"
        });
    };
    SettingsServiceApi.prototype.removeHost = function (id) {
        $.ajax({
            url: this.serviceUrl + "/api/hosts/" + id,
            method: "DELETE"
        });
    };
    return SettingsServiceApi;
}());
var settingsServiceApi = new SettingsServiceApi();
var generalSettings = {
    delay: ko.observable(null),
    disallow: ko.observable(""),
    urls: ko.observableArray([]),
    newUrl: ko.observable(""),
    saveSettings: function () {
        var disallow = generalSettings.disallow();
        var delay = generalSettings.delay();
        settingsServiceApi.saveSettings(disallow, delay);
    },
    addUrl: function () {
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
    removeUrl: function () {
        if (confirm("Are you sure?")) {
            var urls = generalSettings.urls();
            for (var i = 0; i < urls.length; i++) {
                var removed = urls[i];
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
    editHost: function () {
        alert("edit");
    },
    removeHost: function () {
        if (confirm("Are you sure?")) {
            var hosts = hostsDetails.hosts();
            for (var i = 0; i < hosts.length; i++) {
                var removed = hosts[i];
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
//# sourceMappingURL=general-settings.js.map