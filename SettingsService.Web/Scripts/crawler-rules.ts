/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming

class CrawlRule {
    Id: string;
    Host: string;
    DataType: string;
    Name: string;
    RegExpression: string;
    
    constructor(id: string, name?: string, dataType?: string, expression?: string, host?: string) {
        this.Id = id;
        if (name) this.Name = name;
        if (dataType) this.DataType = dataType;
        if (expression) this.RegExpression = expression;
        if (host) this.Host = host;
    }
}

// ReSharper restore InconsistentNaming

class CrawlerRulesApi extends ServiceApi {
    loadCrawlerRules() {
        $.get(this.serviceUrl + "/api/crawler/rules", (data: CrawlRule[]) => {
            if (data != null) {
                crawlerRules.rules(data);
            }
        });
    }
}
var crawlerRulesApi = new CrawlerRulesApi();

class CrawlerRulesViewModel {
    rules: any; 

    constructor(rules: CrawlRule[]) {
        this.rules = ko.observableArray(rules);
    }

    addRules(rule: CrawlRule) {
        this.rules.push(rule);
    }

    removeRule(rule: CrawlRule) {
        this.rules.remove(rule);
    }
}

var crawlerRules = new CrawlerRulesViewModel([
    new CrawlRule("1", "Item1", "Video", "expression1", "url"),
    new CrawlRule("2", "Item2", "Picture", "expression2", "url")
]);