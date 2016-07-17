using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace FhdSettings.Impl.Repositories
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

        public CrawlHostSetting GetHostSettings(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                var settings = ctx.CrawlHostSettings.AsNoTracking().SingleOrDefault(s => s.Host == host);
                return settings;
            }
        }

        public void RemoveHostSettings(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                var settings = ctx.CrawlHostSettings.SingleOrDefault(s => s.Host == host);
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