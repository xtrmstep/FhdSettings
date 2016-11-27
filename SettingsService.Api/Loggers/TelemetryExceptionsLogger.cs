using System.Collections.Generic;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace SettingsService.Api.Loggers
{
    /// <summary>
    ///     Handles all unhandled exception in API
    /// </summary>
    /// <remarks>
    /// More about loggers here https://www.asp.net/web-api/overview/error-handling/web-api-global-error-handling
    /// More about telemetry loggers https://docs.microsoft.com/en-us/azure/application-insights/app-insights-asp-net-exceptions#exceptions
    /// </remarks>
    public class TelemetryExceptionsLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var telemetry = new TelemetryClient();
            var properties = new Dictionary<string, string>
            {
                {"Type", context.Exception.GetType().Name},
                {"Message", context.Exception.Message},
                {"StackTrace", context.Exception.StackTrace}
            };
            if (context.Exception.InnerException != null)
                properties.Add("Inner Exception", context.Exception.InnerException.ToString());

            telemetry.TrackException(context.Exception, properties);
        }
    }
}