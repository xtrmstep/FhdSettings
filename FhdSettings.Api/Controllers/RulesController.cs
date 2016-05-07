using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FhdSettings.Api.Controllers.Attributes;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Controllers
{
    [ValidateAuthToken]
    public class RulesController : ApiController
    {
        private ICrawlerRepository crawlerRepository;

        public RulesController(ICrawlerRepository crawlerRepository)
        {
            this.crawlerRepository = crawlerRepository;
        }

        public IEnumerable<CrawlRule> Get(string host)
        {
            return crawlerRepository.GetRules(host);
        }

        public CrawlRule Get(Guid id)
        {
            return crawlerRepository.GetRule(id);
        }

        public void Post([FromBody]CrawlRule rule)
        {
            crawlerRepository.AddRule(rule);
        }

        public void Put(Guid id, [FromBody]CrawlRule rule)
        {
            crawlerRepository.UpdateRule(rule);
        }

        public void Delete(Guid id)
        {
            crawlerRepository.RemoveRule(id);
        }
    }
}
