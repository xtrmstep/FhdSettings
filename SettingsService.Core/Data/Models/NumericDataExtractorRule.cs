using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettingsService.Core.Data.Models
{
    /// <summary>
    ///     Rule to extract some numeric data
    /// </summary>
    /// <remarks>Numeric data is as follows: number of likes, number of comments, etc. The rule is specific for hosts</remarks>
    public class NumericDataExtractorRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string ExtractorExpression { get; set; }
        public string Host { get; set; }
        public NumericDataType DataType { get; set; }
    }
}