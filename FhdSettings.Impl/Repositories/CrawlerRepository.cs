using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FhdSettings.Data;

namespace FhdSettings.Impl.Repositories
{
    class CrawlerRepository : ICrawlerRepository
    {
        public void AddHostSettings(CrawlHostSetting hostSettings)
        {
            throw new NotImplementedException();
        }

        public void AddRule(CrawlRule rule)
        {
            throw new NotImplementedException();
        }

        public void AddSeedUrl(string url)
        {
            throw new NotImplementedException();
        }

        public CrawlHostSetting GetHostSettings(string url)
        {
            throw new NotImplementedException();
        }

        public CrawlRule GetRule(Guid ruleId)
        {
            throw new NotImplementedException();
        }

        public IList<CrawlRule> GetRules(string host)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetSeedUrls()
        {
            throw new NotImplementedException();
        }

        public void RemoveHostSettings(string url)
        {
            throw new NotImplementedException();
        }

        public void RemoveRule(Guid ruleId)
        {
            throw new NotImplementedException();
        }

        public void RemoveSeedUrl(string url)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostSettings(CrawlHostSetting hostSettings)
        {
            throw new NotImplementedException();
        }

        public void UpdateRule(CrawlRule rule)
        {
            throw new NotImplementedException();
        }
    }
}
