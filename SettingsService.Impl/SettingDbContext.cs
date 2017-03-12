using System.Data.Entity;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl
{
    public class SettingDbContext : DbContext
    {
        public SettingDbContext() : base("SettingsServiceDb")
        {

        }

        public DbSet<ExtractRule> ExtractRules { get; set; }
        public DbSet<HostSetting> HostSettings { get; set; }
        public DbSet<Host> Hosts { get; set; }
    }
}