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
            context.Settings.AddOrUpdate(p => p.Code, new Setting {Code = Settings.CrawlDelay, Name = "CrawlDelay", Value = "60"});
            context.Settings.AddOrUpdate(p => p.Code, new Setting {Code = Settings.Disallow, Name = "Disallow", Value = "*"});
        }
    }
}