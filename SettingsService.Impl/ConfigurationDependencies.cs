using Autofac;
using SettingsService.Core.Data;
using SettingsService.Impl.Repositories;

namespace SettingsService.Impl
{
    public class ConfigurationDependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HostsRepository>().As<IHostsRepository>().InstancePerRequest();
            builder.RegisterType<RulesRepository>().As<IRulesRepository>().InstancePerRequest();
            builder.RegisterType<SettingsRepository>().As<ISettingsRepository>().InstancePerRequest();
        }
    }
}