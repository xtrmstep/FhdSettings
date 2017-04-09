using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettingsService.Core.Data.Models
{
    /// <summary>
    ///     Description of one crawler rule
    /// </summary>
    /// <remarks>It describes a regex expression and a type of data</remarks>
    public class ExtractRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public ExtratorDataType DataType { get; set; }
        public string RegExpression { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ExtractRule;
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