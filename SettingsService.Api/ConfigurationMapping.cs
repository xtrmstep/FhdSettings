using System;
using AutoMapper;
using SettingsService.Api.Models;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api
{
    public class ConfigurationMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<ExtractRule, ExtractRuleReadModel>();

            CreateMap<HostModel, Host>();
            CreateMap<ExtractRuleModel, ExtractRule>()
                .ForMember(dest => dest.DataType, o => o.MapFrom(src => (ExtratorDataType)Enum.Parse(typeof(ExtratorDataType), src.DataType)));
            CreateMap<SettingModel, Setting>();
        }
    }
}