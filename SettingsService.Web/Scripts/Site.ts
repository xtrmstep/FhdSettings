/// <reference path="typings/jquery/jquery.d.ts" />

interface IBuilderDefaultCrawlerSettings {
    (disallow: string, delay: number): void
}

interface IBuilderFrontierSettings {
    (urls: string[]): void
}

class CrawlHostSetting {
    id: string;
    host: string;
    disallow: string;
    crawlDelay: number;
}

class SettingsServiceApi {
    serviceUrl: string;

    constructor(baseServiceUrl: string) {
        this.serviceUrl = baseServiceUrl;
    }

    loadGeneralSettings(buildDefaultSettings: IBuilderDefaultCrawlerSettings,
        buildFrontierSettings: IBuilderFrontierSettings) {
        $.get(this.serviceUrl + "/api/hosts?host=''", (data: CrawlHostSetting) => { buildDefaultSettings(data.disallow, data.crawlDelay); });
        $.get(this.serviceUrl + "/api/urls", (data: string[]) => { buildFrontierSettings(data); });
    }
}