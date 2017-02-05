using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SettingsService.Api.Models
{
    public class HostDefaultSettings
    {
        public int Delay { get; set; }
        public string Disallow { get; set; }
    }
}