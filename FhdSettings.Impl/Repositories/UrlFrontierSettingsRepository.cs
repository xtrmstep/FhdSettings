using System.Collections.Generic;
using System.Linq;
using FhdSettings.Data;

namespace FhdSettings.Impl.Repositories
{
    internal class UrlFrontierSettingsRepository : IUrlFrontierSettingsRepository
    {
        public void AddSeedUrl(string url)
        {
            using (var ctx = new SettingDbContext())
            {
                var seedItem = ctx.CrawlUrlSeeds.Create();
                seedItem.Url = url;
                ctx.CrawlUrlSeeds.Add(seedItem);
                ctx.SaveChanges();
            }
        }

        public IList<string> GetSeedUrls()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlUrlSeeds.AsNoTracking().Select(s => s.Url).ToList();
            }
        }

        public void RemoveSeedUrl(string url)
        {
            using (var ctx = new SettingDbContext())
            {
                var dbUrl = ctx.CrawlUrlSeeds.SingleOrDefault(s => s.Url == url);
                ctx.CrawlUrlSeeds.Remove(dbUrl);
                ctx.SaveChanges();
            }
        }
    }
}