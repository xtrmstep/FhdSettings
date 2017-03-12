using System;
using System.Collections.Generic;
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

        public Guid AddRule(ExtractRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var newRule = ctx.ExtractRules.Create();
                _mapper.Map(rule, newRule);
                ctx.ExtractRules.Add(newRule);
                ctx.SaveChanges();
                return newRule.Id;
            }
        }

        public ExtractRule GetRule(Guid ruleId)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.ExtractRules.AsNoTracking().SingleOrDefault(r => r.Id == ruleId);
            }
        }

        public IList<ExtractRule> GetRules(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.ExtractRules.AsNoTracking().Where(r => r.Host.SeedUrl == host).ToList();
            }
        }

        public void RemoveRule(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var rule = ctx.ExtractRules.SingleOrDefault(s => s.Id == id);
                ctx.ExtractRules.Remove(rule);
                ctx.SaveChanges();
            }
        }

        public void UpdateRule(ExtractRule rule)
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
    }
}