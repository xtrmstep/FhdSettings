using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettingsService.Core.Data.Models
{
    /// <summary>
    ///     Settings for a host
    /// </summary>
    /// <remarks>
    ///     Initial host settings are not tied to a host.
    ///     With the first request a crawler should check robots.txt and update the settings for the host.
    /// </remarks>
    public class Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}