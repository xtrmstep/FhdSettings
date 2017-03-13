using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Impl.Repositories
{
    internal class RulesRepository : IRulesRepository
    {
        private readonly IMapper _mapper;

        public RulesRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IList<ExtractRule> Get()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.ExtractRules.AsNoTracking().ToList();
            }
        }

        public ExtractRule Get(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.ExtractRules.AsNoTracking().SingleOrDefault(r => r.Id == id);
            }
        }

        public Guid Add(ExtractRule item)
        {
            using (var ctx = new SettingDbContext())
            {
                var newRule = ctx.ExtractRules.Create();
                _mapper.Map(item, newRule);
                ctx.ExtractRules.Add(newRule);
                ctx.SaveChanges();
                return newRule.Id;
            }
        }

        public void Update(ExtractRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.ExtractRules.SingleOrDefault(s => s.Id == rule.Id);
                if (existing != null)
                {
                    _mapper.Map(rule, existing);
                    ctx.SaveChanges();
                }
            }
        }

        public void Remove(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.ExtractRules.SingleOrDefault(s => s.Id == id);
                ctx.ExtractRules.Remove(existing);
                ctx.SaveChanges();
            }
        }
    }
}