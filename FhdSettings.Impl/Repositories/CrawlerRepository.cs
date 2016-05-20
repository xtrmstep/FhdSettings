using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    internal class CrawlerRepository : ICrawlerRepository
    {
        private readonly IMapper _mapper;

        public CrawlerRepository(IMapper mapper)
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

        public CrawlHostSetting GetHostSettings(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                var settings = ctx.CrawlHostSettings.AsNoTracking().SingleOrDefault(s => s.Host == host);
                return settings;
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

        public IList<string> GetSeedUrls()
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.CrawlUrlSeeds.AsNoTracking().Select(s => s.Url).ToList();
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

        public void RemoveRule(Guid ruleId)
        {
            using (var ctx = new SettingDbContext())
            {
                var rule = ctx.CrawlRules.SingleOrDefault(s => s.Id == ruleId);
                ctx.CrawlRules.Remove(rule);
                ctx.SaveChanges();
            }
        }

        public void RemoveSeedUrl(string url)
        {
            using (var ctx = new SettingDbContext())
            {
                var dbUrl = ctx.CrawlUrlSeeds.SingleOrDefault(s => s.Url == url);
                ctx.CrawlUrlSeeds.Remove(dbUrl);
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