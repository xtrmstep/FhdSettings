using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettingsService.Core.Data.Models
{
    /// <summary>
    ///     Descriptor of the first URL to start crawling
    /// </summary>
    public class Host
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string SeedUrl { get; set; }
        public DateTimeOffset AddedToSeed { get; set; }
    }
}