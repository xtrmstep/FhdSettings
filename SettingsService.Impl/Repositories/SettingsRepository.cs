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

        public Guid Add(Setting item)
        {
            using (var ctx = new SettingDbContext())
            {
                var newSetting = ctx.Settings.Create();
                _mapper.Map(item, newSetting);
                ctx.Settings.Add(newSetting);
                ctx.SaveChanges();
                return newSetting.Id;
            }
        }

        public Setting Get(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.Settings.AsNoTracking().SingleOrDefault(s => s.Id == id);
            }
        }

        public IList<Setting> Get()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.Settings.AsNoTracking().ToList();
            }
        }

        public void Remove(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var setting = ctx.Settings.SingleOrDefault(s => s.Id == id);
                ctx.Settings.Remove(setting);
                ctx.SaveChanges();
            }
        }

        public void Update(Setting item)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.Settings.SingleOrDefault(s => s.Id == item.Id);
                if (existing != null)
                {
                    _mapper.Map(item, existing);
                    ctx.SaveChanges();
                }
            }
        }
    }
}