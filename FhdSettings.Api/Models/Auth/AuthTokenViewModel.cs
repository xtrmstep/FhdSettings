using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FhdSettings.Api.Models.Auth
{
    public class AuthTokenViewModel
    {
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}