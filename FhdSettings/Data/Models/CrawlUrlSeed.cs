using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FhdSettings.Data.Models
{
    /// <summary>
    ///     Descriptor of the first URL to start crawling
    /// </summary>
    public class CrawlUrlSeed
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Url { get; set; }
    }
}