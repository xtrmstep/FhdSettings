using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Repositories
{
    internal class SettingsRepository : ISettingsRepository
    {
        private readonly IMapper _mapper;

        public SettingsRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Guid AddHostSettings(HostSetting hostSettings)
        {
            using (var ctx = new SettingDbContext())
            {
                var newHostSettings = ctx.HostSettings.Create();
                _mapper.Map(hostSettings, newHostSettings);
                ctx.HostSettings.Add(newHostSettings);
                ctx.SaveChanges();
                return newHostSettings.Id;
            }
        }

        public HostSetting GetHostSettings(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var crawlHostSettings = ctx.HostSettings.AsNoTracking();
                return id == Guid.Empty 
                    ? crawlHostSettings.SingleOrDefault(s => s.Host == null) 
                    : crawlHostSettings.SingleOrDefault(s => s.Id == id);
            }
        }

        public void RemoveHostSettings(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var settings = ctx.HostSettings.SingleOrDefault(s => s.Id == id);
                ctx.HostSettings.Remove(settings);
                ctx.SaveChanges();
            }
        }

        public void UpdateHostSettings(HostSetting hostSettings)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.HostSettings.SingleOrDefault(s => s.Id == hostSettings.Id);
                if (existing != null)
                {
                    _mapper.Map(hostSettings, existing);
                    ctx.SaveChanges();
                }
            }
        }
    }
}