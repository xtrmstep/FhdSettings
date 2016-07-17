using AutoMapper;
using SettingsService.Core.Data.Models;

namespace FhdSettings.Impl
{
    public class ConfigurationMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<CrawlRule, CrawlRule>();
            CreateMap<ClinetService, ClinetService>();
            CreateMap<AuthToken, AuthToken>();
            CreateMap<CrawlHostSetting, CrawlHostSetting>();
            CreateMap<CrawlUrlSeed, CrawlUrlSeed>();
            CreateMap<NumericDataExtractorRule, NumericDataExtractorRule>();
        }
    }
}