using AutoMapper;
using SettingsService.Api.Models;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api
{
    public class ConfigurationMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<HostModel, Host>();

            CreateMap<ExtractRuleCreateModel, ExtractRule>();
            CreateMap<ExtractRuleUpdateModel, ExtractRule>();

            CreateMap<SettingModel, Setting>();
            CreateMap<SettingUpdateModel, Setting>();
        }
    }
}