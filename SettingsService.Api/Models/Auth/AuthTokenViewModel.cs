using System;

namespace SettingsService.Api.Models.Auth
{
    public class AuthTokenViewModel
    {
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}