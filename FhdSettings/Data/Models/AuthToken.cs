using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhdSettings.Data.Models
{
    public class AuthToken
    {
        [ForeignKey("Service")]
        public string ServiceCode{ get; set; }
        public ClinetService Service { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}
