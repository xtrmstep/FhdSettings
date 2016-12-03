using System;
using System.Collections.Generic;
using System.Linq;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Repositories
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

        public IList<CrawlUrlSeed> GetSeedUrls()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlUrlSeeds.AsNoTracking().ToList();
            }
        }

        public CrawlUrlSeed GetUrl(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlUrlSeeds.AsNoTracking().SingleOrDefault(s => s.Id == id);
            }
        }

        public void RemoveSeedUrl(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var dbUrl = ctx.CrawlUrlSeeds.SingleOrDefault(s => s.Id == id);
                ctx.CrawlUrlSeeds.Remove(dbUrl);
                ctx.SaveChanges();
            }
        }
    }
}