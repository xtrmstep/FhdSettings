using System;

namespace SettingsService.Api.Models
{
    /// <summary>
    /// Update model of extract rule
    /// </summary>
    public class ExtractRuleUpdateModel : ExtractRuleCreateModel
    {
        public Guid Id { get; set; }
    }
}