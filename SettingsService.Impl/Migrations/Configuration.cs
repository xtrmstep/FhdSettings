using System;
using System.Data.Entity.Migrations;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SettingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SettingDbContext context)
        {
            context.CrawlHostSettings.AddOrUpdate(
                p => p.Host,
                new CrawlHostSetting
                {
                    Host = string.Empty,
                    CrawlDelay = 60,
                    Disallow = "*"
                });
        }
    }
}