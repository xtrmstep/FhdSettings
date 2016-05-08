using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FhdSettings.Data.Models
{
    /// <summary>
    ///     Description of one crawler rule
    /// </summary>
    /// <remarks>It describes a regex expression and a type of data</remarks>
    public class CrawlRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; }
        public string Name { get; set; }
        public CrawlDataBlockType DataType { get; set; }
        public string RegExpression { get; set; }
        public string Host { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as CrawlRule;
            if (obj == null || other == null) { return false; }

            return Id == other.Id
                   && Name == other.Name
                   && DataType == other.DataType
                   && RegExpression == other.RegExpression;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    /// <summary>
    ///     Type of crawled data
    /// </summary>
    public enum CrawlDataBlockType
    {
        Link,
        Video,
        Picture
    }

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