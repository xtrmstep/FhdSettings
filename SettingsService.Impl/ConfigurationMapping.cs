using AutoMapper;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl
{
    public class ConfigurationMapping : Profile
    {
        protected override void Configure()
        {
            // this mapping is used for quick assignment from parameters to tracked objects inside repositories
            CreateMap<ExtractRule, ExtractRule>();
            CreateMap<Setting, Setting>();
            CreateMap<Host, Host>();
        }
    }
}