/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming

class Setting {
    Id: string;
    Code: string;
    Name: string;
    Value: string;

    compare(other: Setting) {
        return this.Id === other.Id;
    }
}

class SettingsViewModel {
    settings: any;
    settingsApi: SettingsApi;
    editId: any;
    editCode: any;
    editName: any;
    editValue: any;

    constructor(baseServiceUrl: string, settings: Setting[]) {
        this.settings = ko.observableArray(settings);
        this.settingsApi = new SettingsApi(baseServiceUrl);

        this.editId = ko.observable("");
        this.editCode = ko.observable("");
        this.editName = ko.observable("");
        this.editValue = ko.observable("");
    }

    load() {
        this.settingsApi.load((data: Setting[]) => {
            if (data != null && data.length > 0) {
                this.settings(data);
            }
        });
    }

    add() {
        // clear values in inputs
        this.editId("");
        this.editCode("");
        this.editName("");
        this.editValue("");
    }

    save() {
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
            this.settingsApi.save(item, () => {
                var currentArray = this.settings();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        this.settings.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.settingsApi.add(item, () => { this.settings.push(item); });

    }

    delete(model: SettingsViewModel, setting: Setting) {
        if (confirm("Are you sure?")) {
            model.settingsApi.remove(setting,
            () => { model.settings.remove(setting); },
            (error) => { alert(error); });
        }
    }

    edit(model: SettingsViewModel, setting: Setting) {
        // set values to inputs
        model.editId(setting.Id);
        model.editCode(setting.Code);
        model.editName(setting.Name);
        model.editValue(setting.Value);
    }
}

class Host {
    Id: string;
    SeedUrl: string;

    compare(other: Setting) {
        return this.Id === other.Id;
    }
}

class HostsViewModel {
    hosts: any;
    hostsApi: HostsApi;
    editId: any;
    editUrl: any;

    constructor(baseServiceUrl: string, hosts: Host[]) {
        this.hosts = ko.observableArray(hosts);
        this.hostsApi = new HostsApi(baseServiceUrl);

        this.editId = ko.observable("");
        this.editUrl = ko.observable("");
    }

    load() {
        this.hostsApi.load((data: Host[]) => {
            if (data != null && data.length > 0) {
                this.hosts(data);
            }
        });
    }

    add() {
        this.editId("");
        this.editUrl("");
    }

    save() {
        // take values from inputs and store it in API
        var item = new Host();
        item.Id = this.editId();
        item.SeedUrl = this.editUrl();

        if(item.Id)
            this.hostsApi.save(item, () => {
                var currentArray = this.hosts();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        this.hosts.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.hostsApi.add(item, () => { this.hosts.push(item); });
    }

    delete(model: HostsViewModel, host: Host) {
        if (confirm("Are you sure?")) {
            model.hostsApi.remove(host,
            () => { model.hosts.remove(host); },
            (error) => { alert(error); });
        }
    }

    edit(model: HostsViewModel, host: Host) {
        // set values to inputs
        model.editId(host.Id);
        model.editUrl(host.SeedUrl);
    }
}

class Rule {
    Id: string;
    Name: string;
    DataType: string;
    RegExpression: string;

    compare(other: Setting) {
        return this.Id === other.Id;
    }
}

class RulesViewModel {
    rules: any;
    rulesApi: RulesApi;
    editId: any;
    editName: any;
    editDataType: any;
    editExpression: any;

    constructor(baseServiceUrl: string, rules: Rule[]) {
        this.rules = ko.observableArray(rules);
        this.rulesApi = new RulesApi(baseServiceUrl);

        this.editId = ko.observable("");
        this.editName = ko.observable("");
        this.editDataType = ko.observable("");
        this.editExpression = ko.observable("");
    }

    load() {
        this.rulesApi.load((data: Rule[]) => {
            if (data != null && data.length > 0) {
                this.rules(data);
            }
        });
    }

    add() {
        this.editId("");
        this.editName("");
        this.editDataType("");
        this.editExpression("");
    }

    save() {
        // take values from inputs and store it in API
        var item = new Rule();
        item.Id = this.editId();
        item.Name = this.editName();
        item.DataType = this.editDataType();
        item.RegExpression = this.editExpression();

        if (item.Id)
            this.rulesApi.save(item, () => {
                var currentArray = this.rules();
                for (var idx = 0; idx < currentArray.length; idx++) {
                    var old = currentArray[idx];
                    if (item.compare(old)) {
                        this.rules.replace(old, item);
                        break;
                    }
                }
            });
        else
            this.rulesApi.add(item, () => { this.rules.push(item); });
    }

    delete(model: RulesViewModel, rule: Rule) {
        if (confirm("Are you sure?")) {
            model.rulesApi.remove(rule,
            () => { model.rules.remove(rule); },
            (error) => { alert(error); });
        }
    }

    edit(model: RulesViewModel, rule: Rule) {
        // set values to inputs
        model.editId(rule.Id);
        model.editName(rule.Name);
        model.editDataType(rule.DataType);
        model.editExpression(rule.RegExpression);
    }
}

// ReSharper restore InconsistentNaming

class SettingsApi extends ServiceApi {

    constructor(baseServiceUrl: string) {
        super(baseServiceUrl);
    }

    load(callback: (data: Setting[]) => any) {
        var url = this.serviceUrl + "/api/settings";
        this.getAjax(url, callback);
    }

    add(setting: Setting, callback: () => any) {
        var jsonValue: string = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings";
        this.postAjax(url, jsonValue, callback);
    }

    save(setting: Setting, callback: () => any) {
        var jsonValue: string = JSON.stringify(setting);
        var url = this.serviceUrl + "/api/settings/" + setting.Id;
        this.putAjax(url, jsonValue, callback);
    }

    remove(setting: Setting, success: () => any, error: (errorMessage: string) => any) {
        alert("api call to remove setting " + setting.Code);
        success();
        error("error message");
    }
}

class HostsApi extends ServiceApi {

    viewModel: HostsViewModel;

    constructor(baseServiceUrl: string) {
        super(baseServiceUrl);
    }

    load(callback: (data: Host[]) => any) {
        var url = this.serviceUrl + "/api/hosts";
        this.getAjax(url, callback);
    }

    add(host: Host, callback: () => any) {
        var jsonValue: string = JSON.stringify(host);
        var url = this.serviceUrl + "/api/hosts";
        this.postAjax(url, jsonValue, callback);
    }

    save(host: Host, callback: () => any) {
        var jsonValue: string = JSON.stringify(host);
        var url = this.serviceUrl + "/api/hosts/" + host.Id;
        this.putAjax(url, jsonValue, callback);
    }

    remove(host: Host, success: () => any, error: (errorMessage: string) => any) {
        alert("api call to remove host " + host.SeedUrl);
        success();
        error("error message");
    }
}

class RulesApi extends ServiceApi {

    viewModel: RulesViewModel;

    constructor(baseServiceUrl: string) {
        super(baseServiceUrl);
    }

    load(callback: (data: Rule[]) => any) {
        var url = this.serviceUrl + "/api/rules";
        this.getAjax(url, callback);
    }

    add(rule: Rule, callback: () => any) {
        var jsonValue: string = JSON.stringify(rule);
        var url = this.serviceUrl + "/api/rules";
        this.postAjax(url, jsonValue, callback);
    }

    save(rule: Rule, callback: () => any) {
        var jsonValue: string = JSON.stringify(rule);
        var url = this.serviceUrl + "/api/rules/" + rule.Id;
        this.putAjax(url, jsonValue, callback);
    }

    remove(rule: Rule, success: () => any, error: (errorMessage: string) => any) {
        alert("api call to remove rule " + rule.Name);
        success();
        error("error message");
    }
}
