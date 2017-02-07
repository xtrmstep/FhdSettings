/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
var HostsInfo = (function () {
    function HostsInfo() {
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
    SettingsServiceApi.prototype.loadCrawlerRules = function () {
        $.get(this.serviceUrl + "/api/crawler/rules", function (data) {
            if (data != null) {
                crawlerRules.rules(data);
            }
        });
    };
    SettingsServiceApi.prototype.saveSettings = function (hostInfo, callback) {
        var jsonValue = JSON.stringify(hostInfo);
        $.ajax({
            url: this.serviceUrl + "/api/hosts/" + hostInfo.Id,
            method: "PUT",
            data: jsonValue,
            contentType: "application/json",
            success: function () {
                callback();
            }
        });
    };
    SettingsServiceApi.prototype.saveDefaultSettings = function (disallow, delay) {
        var jsonValue = JSON.stringify({
            disallow: disallow,
            delay: delay
        });
        $.ajax({
            url: this.serviceUrl + "/api/hosts/default",
            method: "PUT",
            data: jsonValue,
            contentType: "application/json"
        });
    };
    SettingsServiceApi.prototype.addUrl = function (urlInfo) {
        var jsonValue = JSON.stringify({
            Url: urlInfo.Url
        });
        $.ajax({
            url: this.serviceUrl + "/api/urls",
            method: "POST",
            data: jsonValue,
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
