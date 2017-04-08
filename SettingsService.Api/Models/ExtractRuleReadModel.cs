using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Models
{
    /// <summary>
    /// Create model of extract rule
    /// </summary>
    public class ExtractRuleReadModel : ExtractRuleModel
    {
        public Guid Id { get; set; }
    }
}