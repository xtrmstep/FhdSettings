/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
// ReSharper disable InconsistentNaming
var CrawlRule = (function () {
    function CrawlRule(id, name, dataType, expression, host) {
        this.Id = id;
        if (name)
            this.Name = name;
        if (dataType)
            this.DataType = dataType;
        if (expression)
            this.RegExpression = expression;
        if (host)
            this.Host = host;
    }
    return CrawlRule;
}());
// ReSharper restore InconsistentNaming
var CrawlerRulesApi = (function (_super) {
    __extends(CrawlerRulesApi, _super);
    function CrawlerRulesApi() {
        _super.apply(this, arguments);
    }
    CrawlerRulesApi.prototype.loadCrawlerRules = function () {
        $.get(this.serviceUrl + "/api/crawler/rules", function (data) {
            if (data != null) {
                crawlerRules.rules(data);
            }
        });
    };
    return CrawlerRulesApi;
}(ServiceApi));
var crawlerRulesApi = new CrawlerRulesApi();
var CrawlerRulesViewModel = (function () {
    function CrawlerRulesViewModel(rules) {
        this.rules = ko.observableArray(rules);
    }
    CrawlerRulesViewModel.prototype.addRules = function (rule) {
        this.rules.push(rule);
    };
    CrawlerRulesViewModel.prototype.removeRule = function (rule) {
        this.rules.remove(rule);
    };
    return CrawlerRulesViewModel;
}());
var crawlerRules = new CrawlerRulesViewModel([
    new CrawlRule("1", "Item1", "Video", "expression1", "url"),
    new CrawlRule("2", "Item2", "Picture", "expression2", "url")
]);
//# sourceMappingURL=crawler-rules.js.map