/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />

// ReSharper disable InconsistentNaming
class CrawlRule {
    Id: string;
    Name: string;
    DataType: string;
    RegExpression: string;
    Host: string;
}
// ReSharper restore InconsistentNaming

var crawlerRules = {
    rules: ko.observableArray([])
};