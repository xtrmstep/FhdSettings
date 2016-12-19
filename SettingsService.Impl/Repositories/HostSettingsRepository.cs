using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Repositories
{
    internal class HostSettingsRepository : IHostSettingsRepository
    {
        private readonly IMapper _mapper;

        public HostSettingsRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddHostSettings(CrawlHostSetting hostSettings)
        {
            using (var ctx = new SettingDbContext())
            {
                var newHostSettings = ctx.CrawlHostSettings.Create();
                _mapper.Map(hostSettings, newHostSettings);
                ctx.CrawlHostSettings.Add(newHostSettings);
                ctx.SaveChanges();
            }
        }

        public IList<CrawlHostSetting> GetHostSettings()
        {
            using (var ctx = new SettingDbContext())
            {
                // return all items, without default one
                var settings = ctx.CrawlHostSettings.AsNoTracking().Where(s => s.Host != string.Empty).ToList();
                return settings;
            }
        }

        public CrawlHostSetting GetHostSettings(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var crawlHostSettings = ctx.CrawlHostSettings.AsNoTracking();
                return id == Guid.Empty 
                    ? crawlHostSettings.SingleOrDefault(s => s.Host == string.Empty) 
                    : crawlHostSettings.SingleOrDefault(s => s.Id == id);
            }
        }

        public void RemoveHostSettings(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var settings = ctx.CrawlHostSettings.SingleOrDefault(s => s.Id == id);
                ctx.CrawlHostSettings.Remove(settings);
                ctx.SaveChanges();
            }
        }

        public void UpdateHostSettings(CrawlHostSetting hostSettings)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.CrawlHostSettings.SingleOrDefault(s => s.Id == hostSettings.Id);
                if (existing != null)
                {
                    _mapper.Map(hostSettings, existing);
                    ctx.SaveChanges();
                }
            }
        }
    }
}