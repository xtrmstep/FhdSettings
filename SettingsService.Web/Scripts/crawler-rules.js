/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
// ReSharper disable InconsistentNaming
var CrawlRule = (function () {
    function CrawlRule() {
    }
    return CrawlRule;
}());
// ReSharper restore InconsistentNaming
var crawlerRules = {
    rules: ko.observableArray([])
};
