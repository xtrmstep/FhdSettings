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

        public Guid AddHost(Host host)
        {
            using (var ctx = new SettingDbContext())
            {
                var newHost = ctx.Hosts.Create();
                _mapper.Map(host, newHost);
                newHost.AddedToSeed = DateTimeOffset.UtcNow;
                ctx.Hosts.Add(newHost);
                ctx.SaveChanges();
                return newHost.Id;
            }
        }

        public void UpdateHost(Host host)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.Hosts.SingleOrDefault(s => s.Id == host.Id);
                if (existing != null)
                {
                    _mapper.Map(host, existing);
                    ctx.SaveChanges();
                }
            }
        }

        public IList<Host> GetHosts()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.Hosts.AsNoTracking().ToList();
            }
        }

        public void RemoveHost(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var dbUrl = ctx.Hosts.SingleOrDefault(s => s.Id == id);
                ctx.Hosts.Remove(dbUrl);
                ctx.SaveChanges();
            }
        }
    }
}