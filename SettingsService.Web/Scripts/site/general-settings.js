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
    function SettingsViewModel(settings, settingsApi) {
        this.settings = ko.observableArray(settings);
        this.settingsApi = settingsApi;
    }
    SettingsViewModel.prototype.add = function (setting) {
        alert("add");
        var s = new Setting();
        s.Id = "1";
        s.Code = "2";
        s.Name = "3";
        s.Value = "4";
        this.settings.push(s);
    };
    SettingsViewModel.prototype.delete = function (setting) {
        if (confirm("Are you sure?")) {
            alert("delete " + setting.Code);
            this.settingsApi.remove(setting, function () {
                alert("OK");
                // this.settings.remove(setting);
            }, function (error) { alert(error); });
        }
    };
    SettingsViewModel.prototype.edit = function (setting) {
        alert("edit " + setting.Code);
    };
    return SettingsViewModel;
}());
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
// ReSharper restore InconsistentNaming
var SettingsApi = (function (_super) {
    __extends(SettingsApi, _super);
    function SettingsApi(baseServiceUrl, settingsViewModel) {
        var _this = _super.call(this, baseServiceUrl) || this;
        _this.viewModel = settingsViewModel;
        return _this;
    }
    SettingsApi.prototype.load = function () {
        var _this = this;
        var url = this.serviceUrl + "/api/settings";
        this.getAjax(url, function (data) {
            if (data != null) {
                _this.viewModel.settings(data);
            }
        });
    };
    SettingsApi.prototype.save = function (setting, callback) {
        var jsonValue = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings/" + setting.Id;
        this.putAjax(url, jsonValue, callback);
    };
    SettingsApi.prototype.remove = function (setting, success, error) {
        alert("api call to remove setting " + setting.Code);
        success();
        error("error message");
    };
    return SettingsApi;
}(ServiceApi));
var HostsApi = (function (_super) {
    __extends(HostsApi, _super);
    function HostsApi(baseServiceUrl, hostsViewModel) {
        var _this = _super.call(this, baseServiceUrl) || this;
        _this.viewModel = hostsViewModel;
        return _this;
    }
    HostsApi.prototype.load = function () {
        var _this = this;
        var url = this.serviceUrl + "/api/hosts";
        this.getAjax(url, function (data) {
            if (data != null) {
                _this.viewModel.hosts(data);
            }
        });
    };
    return HostsApi;
}(ServiceApi));
var RulesApi = (function (_super) {
    __extends(RulesApi, _super);
    function RulesApi(baseServiceUrl, rulesViewModel) {
        var _this = _super.call(this, baseServiceUrl) || this;
        _this.viewModel = rulesViewModel;
        return _this;
    }
    RulesApi.prototype.load = function () {
        var _this = this;
        var url = this.serviceUrl + "/api/rules";
        this.getAjax(url, function (data) {
            if (data != null) {
                _this.viewModel.rules(data);
            }
        });
    };
    return RulesApi;
}(ServiceApi));
//# sourceMappingURL=general-settings.js.map