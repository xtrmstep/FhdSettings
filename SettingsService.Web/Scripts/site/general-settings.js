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
    Setting.prototype.compare = function (other) {
        return this.Id === other.Id;
    };
    return Setting;
}());
var SettingsViewModel = (function () {
    function SettingsViewModel(baseServiceUrl, settings) {
        this.settings = ko.observableArray(settings);
        this.settingsApi = new SettingsApi(baseServiceUrl);
        this.editId = ko.observable("");
        this.editCode = ko.observable("");
        this.editName = ko.observable("");
        this.editValue = ko.observable("");
    }
    SettingsViewModel.prototype.load = function () {
        var _this = this;
        this.settingsApi.load(function (data) {
            if (data != null && data.length > 0) {
                _this.settings(data);
            }
        });
    };
    SettingsViewModel.prototype.add = function () {
        // clear values in inputs
        this.editId("");
        this.editCode("");
        this.editName("");
        this.editValue("");
    };
    SettingsViewModel.prototype.save = function () {
        var _this = this;
        // take values from inputs and store it in API
        var item = new Setting();
        item.Id = this.editId();
        item.Code = this.editCode();
        item.Name = this.editName();
        item.Value = this.editValue();
        // todo update ID for new item stored via API
        // todo delete item via API
        // todo DataType should be show as text
        if (item.Id)
            this.settingsApi.save(item, function () {
                var currentArray = _this.settings();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        _this.settings.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.settingsApi.add(item, function () { _this.settings.push(item); });
    };
    SettingsViewModel.prototype.delete = function (model, setting) {
        if (confirm("Are you sure?")) {
            model.settingsApi.remove(setting, function () { model.settings.remove(setting); }, function (error) { alert(error); });
        }
    };
    SettingsViewModel.prototype.edit = function (model, setting) {
        // set values to inputs
        model.editId(setting.Id);
        model.editCode(setting.Code);
        model.editName(setting.Name);
        model.editValue(setting.Value);
    };
    return SettingsViewModel;
}());
var Host = (function () {
    function Host() {
    }
    Host.prototype.compare = function (other) {
        return this.Id === other.Id;
    };
    return Host;
}());
var HostsViewModel = (function () {
    function HostsViewModel(baseServiceUrl, hosts) {
        this.hosts = ko.observableArray(hosts);
        this.hostsApi = new HostsApi(baseServiceUrl);
        this.editId = ko.observable("");
        this.editUrl = ko.observable("");
    }
    HostsViewModel.prototype.load = function () {
        var _this = this;
        this.hostsApi.load(function (data) {
            if (data != null && data.length > 0) {
                _this.hosts(data);
            }
        });
    };
    HostsViewModel.prototype.add = function () {
        this.editId("");
        this.editUrl("");
    };
    HostsViewModel.prototype.save = function () {
        var _this = this;
        // take values from inputs and store it in API
        var item = new Host();
        item.Id = this.editId();
        item.SeedUrl = this.editUrl();
        if (item.Id)
            this.hostsApi.save(item, function () {
                var currentArray = _this.hosts();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        _this.hosts.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.hostsApi.add(item, function () { _this.hosts.push(item); });
    };
    HostsViewModel.prototype.delete = function (model, host) {
        if (confirm("Are you sure?")) {
            model.hostsApi.remove(host, function () { model.hosts.remove(host); }, function (error) { alert(error); });
        }
    };
    HostsViewModel.prototype.edit = function (model, host) {
        // set values to inputs
        model.editId(host.Id);
        model.editUrl(host.SeedUrl);
    };
    return HostsViewModel;
}());
var Rule = (function () {
    function Rule() {
    }
    Rule.prototype.compare = function (other) {
        return this.Id === other.Id;
    };
    return Rule;
}());
var RulesViewModel = (function () {
    function RulesViewModel(baseServiceUrl, rules) {
        this.rules = ko.observableArray(rules);
        this.rulesApi = new RulesApi(baseServiceUrl);
        this.editId = ko.observable("");
        this.editName = ko.observable("");
        this.editDataType = ko.observable("");
        this.editExpression = ko.observable("");
    }
    RulesViewModel.prototype.load = function () {
        var _this = this;
        this.rulesApi.load(function (data) {
            if (data != null && data.length > 0) {
                _this.rules(data);
            }
        });
    };
    RulesViewModel.prototype.add = function () {
        this.editId("");
        this.editName("");
        this.editDataType("");
        this.editExpression("");
    };
    RulesViewModel.prototype.save = function () {
        var _this = this;
        // take values from inputs and store it in API
        var item = new Rule();
        item.Id = this.editId();
        item.Name = this.editName();
        item.DataType = this.editDataType();
        item.RegExpression = this.editExpression();
        if (item.Id)
            this.rulesApi.save(item, function () {
                var currentArray = _this.rules();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        _this.rules.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.rulesApi.add(item, function () { _this.rules.push(item); });
    };
    RulesViewModel.prototype.delete = function (model, rule) {
        if (confirm("Are you sure?")) {
            model.rulesApi.remove(rule, function () { model.rules.remove(rule); }, function (error) { alert(error); });
        }
    };
    RulesViewModel.prototype.edit = function (model, rule) {
        // set values to inputs
        model.editId(rule.Id);
        model.editName(rule.Name);
        model.editDataType(rule.DataType);
        model.editExpression(rule.RegExpression);
    };
    return RulesViewModel;
}());
// ReSharper restore InconsistentNaming
var SettingsApi = (function (_super) {
    __extends(SettingsApi, _super);
    function SettingsApi(baseServiceUrl) {
        return _super.call(this, baseServiceUrl) || this;
    }
    SettingsApi.prototype.load = function (callback) {
        var url = this.serviceUrl + "/api/settings";
        this.getAjax(url, callback);
    };
    SettingsApi.prototype.add = function (setting, callback) {
        var jsonValue = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings";
        this.postAjax(url, jsonValue, callback);
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
    function HostsApi(baseServiceUrl) {
        return _super.call(this, baseServiceUrl) || this;
    }
    HostsApi.prototype.load = function (callback) {
        var url = this.serviceUrl + "/api/hosts";
        this.getAjax(url, callback);
    };
    HostsApi.prototype.add = function (host, callback) {
        var jsonValue = JSON.stringify(host);
        var url = this.serviceUrl + "/api/hosts";
        this.postAjax(url, jsonValue, callback);
    };
    HostsApi.prototype.save = function (host, callback) {
        var jsonValue = JSON.stringify(host);
        var url = this.serviceUrl + "/api/hosts/" + host.Id;
        this.putAjax(url, jsonValue, callback);
    };
    HostsApi.prototype.remove = function (host, success, error) {
        alert("api call to remove host " + host.SeedUrl);
        success();
        error("error message");
    };
    return HostsApi;
}(ServiceApi));
var RulesApi = (function (_super) {
    __extends(RulesApi, _super);
    function RulesApi(baseServiceUrl) {
        return _super.call(this, baseServiceUrl) || this;
    }
    RulesApi.prototype.load = function (callback) {
        var url = this.serviceUrl + "/api/rules";
        this.getAjax(url, callback);
    };
    RulesApi.prototype.add = function (rule, callback) {
        var jsonValue = JSON.stringify(rule);
        var url = this.serviceUrl + "/api/rules";
        this.postAjax(url, jsonValue, callback);
    };
    RulesApi.prototype.save = function (rule, callback) {
        var jsonValue = JSON.stringify(rule);
        var url = this.serviceUrl + "/api/rules/" + rule.Id;
        this.putAjax(url, jsonValue, callback);
    };
    RulesApi.prototype.remove = function (rule, success, error) {
        alert("api call to remove rule " + rule.Name);
        success();
        error("error message");
    };
    return RulesApi;
}(ServiceApi));
