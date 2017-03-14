/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming

class Setting {
    Id: string;
    Code: string;
    Name: string;
    Value: string;
}

class SettingsViewModel {
    settings: any;

    constructor(settings: Setting[]) {
        this.settings = ko.observableArray(settings);
    }

    add(setting: Setting) {
        this.settings.push(setting);
    }

    remove(setting: Setting) {
        this.settings.remove(setting);
    }
}

var settingsViewModel = new SettingsViewModel([]);

class Host {
    Id: string;
    SeedUrl: string;

    constructor(url: string) {
        this.SeedUrl = url;
    }
}

class HostsViewModel {
    hosts: any;

    constructor(hosts: Host[]) {
        this.hosts = ko.observableArray(hosts);
    }

    add(host: Host) {
        this.hosts.push(host);
    }

    remove(host: Host) {
        this.hosts.remove(host);
    }
}

var hostsViewModel = new HostsViewModel([]);

class Rule {
    Id: string;
    Name: string;
    DataType: string;
    RegExpression: string;

    constructor(id: string, name?: string, dataType?: string, expression?: string) {
        this.Id = id;
        if (name) this.Name = name;
        if (dataType) this.DataType = dataType;
        if (expression) this.RegExpression = expression;
    }
}

class RulesViewModel {
    rules: any;

    constructor(rules: Rule[]) {
        this.rules = ko.observableArray(rules);
    }

    add(rule: Rule) {
        this.rules.push(rule);
    }

    remove(rule: Rule) {
        this.rules.remove(rule);
    }
}

var rulesViewModel = new RulesViewModel([
    new Rule("1", "Item1", "Video", "expression1"),
    new Rule("2", "Item2", "Picture", "expression2")
]);

// ReSharper restore InconsistentNaming

class SettingsApi extends ServiceApi {

    load() {
        var url = this.serviceUrl + "/api/settings";
        this.getAjax(url, (data: Setting[]) => {
            if (data != null) {
                settingsViewModel.settings(data);
            }
        });
    }

    save(setting: Setting, callback: () => any) {
        var jsonValue: string = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings/" + setting.Id;
        this.putAjax(url, jsonValue, callback);
    }
}

var settingsApi = new SettingsApi();

class HostsApi extends ServiceApi {

    load() {
        var url = this.serviceUrl + "/api/hosts";
        this.getAjax(url, (data: Host[]) => {
            if (data != null) {
                hostsViewModel.hosts(data);
            }
        });
    }
}

var hostsApi = new HostsApi();

class RulesApi extends ServiceApi {
    load() {
        var url = this.serviceUrl + "/api/rules";
        this.getAjax(url, (data: Rule[]) => {
            if (data != null) {
                rulesViewModel.rules(data);
            }
        });
    }
}
var rulesApi = new RulesApi();