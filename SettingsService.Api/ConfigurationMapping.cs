using AutoMapper;
using SettingsService.Api.Models;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api
{
    public class ConfigurationMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<HostCreateModel, Host>();

            CreateMap<ExtractRuleCreateModel, ExtractRule>();
            CreateMap<ExtractRuleUpdateModel, ExtractRule>();

            CreateMap<HostSettingCreateModel, HostSetting>();
            CreateMap<HostSettingUpdateModel, HostSetting>();
        }
    }
}