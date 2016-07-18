using System.Data.Entity;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl
{
    public class SettingDbContext : DbContext
    {
        public SettingDbContext() : base("SettingsServiceDb")
        {

        }

        public DbSet<CrawlRule> CrawlRules { get; set; }
        public DbSet<CrawlHostSetting> CrawlHostSettings { get; set; }
        public DbSet<CrawlUrlSeed> CrawlUrlSeeds { get; set; }
        public DbSet<NumericDataExtractorRule> NumericDataExtractorRules { get; set; }
        public DbSet<ClinetService> ClientServices { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
    }
}