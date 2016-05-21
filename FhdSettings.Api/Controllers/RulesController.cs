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
    //[ValidateAuthToken]
    public class RulesController : ApiController
    {
        private readonly ICrawlerRepository _crawlerRepository;

        public RulesController(ICrawlerRepository crawlerRepository)
        {
            _crawlerRepository = crawlerRepository;
        }

        public IEnumerable<CrawlRule> Get(string host)
        {
            return _crawlerRepository.GetRules(host);
        }

        public CrawlRule Get(Guid id)
        {
            return _crawlerRepository.GetRule(id);
        }

        public void Post([FromBody]CrawlRule rule)
        {
            _crawlerRepository.AddRule(rule);
        }

        public void Put(Guid id, [FromBody]CrawlRule rule)
        {
            _crawlerRepository.UpdateRule(rule);
        }

        public void Delete(Guid id)
        {
            _crawlerRepository.RemoveRule(id);
        }
    }
}
