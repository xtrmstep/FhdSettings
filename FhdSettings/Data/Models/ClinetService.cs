using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FhdSettings.Data.Models
{
    public class ClinetService
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTimeOffset? PasswordExpires { get; set; }
    }
}
