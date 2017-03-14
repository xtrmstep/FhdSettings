/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
// ReSharper disable InconsistentNaming
var Setting = (function () {
    function Setting() {
    }
    return Setting;
}());
var SettingsViewModel = (function () {
    function SettingsViewModel(settings) {
        this.settings = ko.observableArray(settings);
    }
    SettingsViewModel.prototype.add = function (setting) {
        this.settings.push(setting);
    };
    SettingsViewModel.prototype.remove = function (setting) {
        this.settings.remove(setting);
    };
    return SettingsViewModel;
}());
var settingsViewModel = new SettingsViewModel([]);
var Host = (function () {
    function Host(url) {
        this.SeedUrl = url;
    }
    return Host;
}());
var HostsViewModel = (function () {
    function HostsViewModel(hosts) {
        this.hosts = ko.observableArray(hosts);
    }
    HostsViewModel.prototype.add = function (host) {
        this.hosts.push(host);
    };
    HostsViewModel.prototype.remove = function (host) {
        this.hosts.remove(host);
    };
    return HostsViewModel;
}());
var hostsViewModel = new HostsViewModel([]);
var Rule = (function () {
    function Rule(id, name, dataType, expression) {
        this.Id = id;
        if (name)
            this.Name = name;
        if (dataType)
            this.DataType = dataType;
        if (expression)
            this.RegExpression = expression;
    }
    return Rule;
}());
var RulesViewModel = (function () {
    function RulesViewModel(rules) {
        this.rules = ko.observableArray(rules);
    }
    RulesViewModel.prototype.add = function (rule) {
        this.rules.push(rule);
    };
    RulesViewModel.prototype.remove = function (rule) {
        this.rules.remove(rule);
    };
    return RulesViewModel;
}());
var rulesViewModel = new RulesViewModel([
    new Rule("1", "Item1", "Video", "expression1"),
    new Rule("2", "Item2", "Picture", "expression2")
]);
// ReSharper restore InconsistentNaming
var SettingsApi = (function (_super) {
    __extends(SettingsApi, _super);
    function SettingsApi() {
        _super.apply(this, arguments);
    }
    SettingsApi.prototype.load = function () {
        var url = this.serviceUrl + "/api/settings";
        this.getAjax(url, function (data) {
            if (data != null) {
                settingsViewModel.settings(data);
            }
        });
    };
    SettingsApi.prototype.save = function (setting, callback) {
        var jsonValue = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings/" + setting.Id;
        this.putAjax(url, jsonValue, callback);
    };
    return SettingsApi;
}(ServiceApi));
var settingsApi = new SettingsApi();
var HostsApi = (function (_super) {
    __extends(HostsApi, _super);
    function HostsApi() {
        _super.apply(this, arguments);
    }
    HostsApi.prototype.load = function () {
        var url = this.serviceUrl + "/api/hosts";
        this.getAjax(url, function (data) {
            if (data != null) {
                hostsViewModel.hosts(data);
            }
        });
    };
    return HostsApi;
}(ServiceApi));
var hostsApi = new HostsApi();
var RulesApi = (function (_super) {
    __extends(RulesApi, _super);
    function RulesApi() {
        _super.apply(this, arguments);
    }
    RulesApi.prototype.load = function () {
        var url = this.serviceUrl + "/api/rules";
        this.getAjax(url, function (data) {
            if (data != null) {
                rulesViewModel.rules(data);
            }
        });
    };
    return RulesApi;
}(ServiceApi));
var rulesApi = new RulesApi();
