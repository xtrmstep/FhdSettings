using System;
using System.ComponentModel.DataAnnotations;

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