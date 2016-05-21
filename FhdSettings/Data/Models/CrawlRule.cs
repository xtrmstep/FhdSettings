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
        public Guid Id { get; set; }

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
}