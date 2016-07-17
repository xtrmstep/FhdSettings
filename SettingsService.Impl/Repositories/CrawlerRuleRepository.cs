using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    internal class CrawlerRuleRepository : ICrawlerRuleRepository
    {
        private readonly IMapper _mapper;

        public CrawlerRuleRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddRule(CrawlRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var newRule = ctx.CrawlRules.Create();
                _mapper.Map(rule, newRule);
                ctx.CrawlRules.Add(newRule);
                ctx.SaveChanges();
            }
        }

        public CrawlRule GetRule(Guid ruleId)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlRules.AsNoTracking().SingleOrDefault(r => r.Id == ruleId);
            }
        }

        public IList<CrawlRule> GetRules(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlRules.AsNoTracking().Where(r => r.Host == host).ToList();
            }
        }

        public void RemoveRule(Guid ruleId)
        {
            using (var ctx = new SettingDbContext())
            {
                var rule = ctx.CrawlRules.SingleOrDefault(s => s.Id == ruleId);
                ctx.CrawlRules.Remove(rule);
                ctx.SaveChanges();
            }
        }

        public void UpdateRule(CrawlRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.CrawlRules.SingleOrDefault(s => s.Id == rule.Id);
                if (existing != null)
                {
                    _mapper.Map(rule, existing);
                    ctx.SaveChanges();
                }
            }
        }
    }
}