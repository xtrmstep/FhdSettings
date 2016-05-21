using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FhdSettings.Data.Models
{
    public class AuthToken
    {
        [Key]
        [ForeignKey("Service")]
        public string ServiceCode { get; set; }

        public ClinetService Service { get; set; }
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}