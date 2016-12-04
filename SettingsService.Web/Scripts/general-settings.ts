/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/site.d.ts" />

interface IBuilderDefaultCrawlerSettings {
    (data: CrawlHostSetting): void
}

interface IBuilderFrontierSettings {
    (urls: CrawlUrlSeed[]): void
}

class SettingsServiceApi {
    serviceUrl: string;

    constructor(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    loadGeneralSettings(buildDefaultSettings: IBuilderDefaultCrawlerSettings,
        buildFrontierSettings: IBuilderFrontierSettings) {
        $.get(this.serviceUrl + "/api/hosts/default", (data: CrawlHostSetting) => {
            buildDefaultSettings(data);
        });
        $.get(this.serviceUrl + "/api/urls", (data: CrawlUrlSeed[]) => {
            buildFrontierSettings(data);
        });
    }
}