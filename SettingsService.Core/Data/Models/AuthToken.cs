using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettingsService.Core.Data.Models
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