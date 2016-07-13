using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    internal class AnalizerRepository : IAnalizerRepository
    {
        private readonly IMapper _mapper;

        public AnalizerRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void AddNumericRule(NumericDataExtractorRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var newRule = ctx.NumericDataExtractorRules.Create();
                _mapper.Map(rule, newRule);
                ctx.NumericDataExtractorRules.Add(newRule);
                ctx.SaveChanges();
            }
        }

        public IList<NumericDataExtractorRule> GetNumericRules(string host)
        {
            using (var ctx = new SettingDbContext())
            {
                return ctx.NumericDataExtractorRules.AsQueryable().Where(r => r.Host == host).ToList();
            }
        }

        public void RemoveNumericRule(Guid id)
        {
            using (var ctx = new SettingDbContext())
            {
                var rule = ctx.NumericDataExtractorRules.SingleOrDefault(r => r.Id == id);
                ctx.NumericDataExtractorRules.Remove(rule);
                ctx.SaveChanges();
            }
        }

        public void UpdateNumericRule(NumericDataExtractorRule rule)
        {
            using (var ctx = new SettingDbContext())
            {
                var existing = ctx.NumericDataExtractorRules.SingleOrDefault(r => r.Id == rule.Id);
                if (existing != null)
                {
                    _mapper.Map(rule, existing);
                    ctx.SaveChanges();
                }
            }
        }
    }
}