using Autofac;
using AutoMapper;
using FhdSettings.Data;
using FhdSettings.Impl.Repositories;

namespace FhdSettings.Impl
{
    public class ConfigurationDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UrlFrontierSettingsRepository>().As<IUrlFrontierSettingsRepository>().InstancePerRequest();
            builder.RegisterType<CrawlerRuleRepository>().As<ICrawlerRuleRepository>().InstancePerRequest();
            builder.RegisterType<AnalizerRepository>().As<IAnalizerRepository>().InstancePerRequest();
            builder.RegisterType<HostSettingsRepository>().As<IHostSettingsRepository>().InstancePerRequest();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<ConfigurationMapping>());
            var mapper = config.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();
        }
    }
}