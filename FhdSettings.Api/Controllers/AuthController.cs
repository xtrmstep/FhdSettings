using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FhdSettings.Api.Models.Auth;

namespace FhdSettings.Api.Controllers
{
    public class AuthController : ApiController
    {
        public AuthController()
        {

        }

        public AuthToken Register(string service, string password)
        {
            return new AuthToken();
        }
    }
}
