using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Repositories
{
    internal class HostsRepository : IHostsRepository
    {
        private readonly IMapper _mapper;
        public HostsRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IList<Host> Get()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.Hosts.AsNoTracking().ToList();
            }
        }

        public Host Get(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.Hosts.AsNoTracking().SingleOrDefault(h => h.Id == id);
            }
        }

        public Guid Add(Host item)
        {
            using (var ctx = new SettingDbContext())
            {
                var newHost = ctx.Hosts.Create();
                _mapper.Map(item, newHost);
                newHost.AddedToSeed = DateTimeOffset.UtcNow;
                ctx.Hosts.Add(newHost);
                ctx.SaveChanges();
                return newHost.Id;
            }
        }

        public void Update(Host item)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.Hosts.SingleOrDefault(s => s.Id == item.Id);
                if (existing != null)
                {
                    _mapper.Map(item, existing);
                    ctx.SaveChanges();
                }
            }
        }

        public void Remove(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.Hosts.SingleOrDefault(s => s.Id == id);
                ctx.Hosts.Remove(existing);
                ctx.SaveChanges();
            }
        }
    }
}