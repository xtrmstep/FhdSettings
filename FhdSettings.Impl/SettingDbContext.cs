using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl
{
    public class SettingDbContext : DbContext
    {
        public DbSet<CrawlRule> CrawlRules { get; set; }
        public DbSet<CrawlHostSetting> CrawlHostSettings { get; set; }
        public DbSet<CrawlUrlSeed> CrawlUrlSeeds { get; set; }
        public DbSet<NumericDataExtractorRule> NumericDataExtractorRules { get; set; }

    }
}
