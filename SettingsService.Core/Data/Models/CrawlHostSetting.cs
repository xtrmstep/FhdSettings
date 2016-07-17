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
    public class CrawlHostSetting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <remarks>It should be unique for whole set</remarks>
        // todo test for uniqueness
        public string Host { get; set; }

        public string Disallow { get; set; }
        public int CrawlDelay { get; set; }
    }
}