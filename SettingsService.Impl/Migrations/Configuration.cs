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
            context.HostSettings.AddOrUpdate(
                p => p.Host,
                new HostSetting
                {
                    Host = null,
                    CrawlDelay = 60,
                    Disallow = "*"
                });
        }
    }
}