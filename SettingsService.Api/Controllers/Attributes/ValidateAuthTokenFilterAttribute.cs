﻿using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SettingsService.Api.Controllers.Attributes
{
    public class ValidateAuthTokenAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //var token = actionContext.Request.Headers.Authorization.Parameter;
            //if (!IsValid(token))
            //{
            //    SimulationRepository.LogEvent("Token X-Fourth-Token is invalid", DateTime.UtcNow);

            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
        }
    }
}