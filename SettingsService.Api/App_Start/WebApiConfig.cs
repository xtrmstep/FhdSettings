using System.Web.Http;
using System.Web.Http.Cors;

namespace SettingsService.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // good explanation of CORS https://www.html5rocks.com/en/tutorials/cors/
            // enable CORS. read more https://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            // detailed clarification https://msdn.microsoft.com/en-us/magazine/dn532203.aspx
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
