using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using FhdSettings.Impl;

namespace FhdSettings.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationDependencies());
                GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
