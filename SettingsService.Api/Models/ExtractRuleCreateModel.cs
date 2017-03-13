using System.Collections.Generic;
using System.Linq;
using System.Web;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Models
{
    /// <summary>
    /// Create model of extract rule
    /// </summary>
    public class ExtractRuleCreateModel
    {
        /// <summary>
        /// Name of the rule
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rule data type
        /// </summary>
        public ExtratorDataType DataType { get; set; }

        /// <summary>
        /// Rule expression
        /// </summary>
        public string RegExpression { get; set; }
    }
}