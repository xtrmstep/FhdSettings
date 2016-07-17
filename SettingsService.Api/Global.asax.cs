using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using SettingsService.Impl;

namespace SettingsService.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Configure(GlobalConfiguration.Configuration);
        }

        public static void Configure(HttpConfiguration config)
        {
            WebApiConfig.Register(config);

            #region autofac
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new ConfigurationDependencies());

            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ConfigurationMapping>();
                cfg.AddProfile<Impl.ConfigurationMapping>();
            });
            var mapper = configMapper.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container); 
            #endregion

            config.EnsureInitialized();
        }
    }
}