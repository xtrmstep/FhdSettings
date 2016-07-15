using System;
using System.Web.Http;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Controllers
{
    [RoutePrefix("api/crawler/rules")]
    public class CrawlerRulesController : ApiController
    {
        private readonly ICrawlerRuleRepository _crawlerRulesRepository;

        public CrawlerRulesController(ICrawlerRuleRepository crawlerRulesRepository)
        {
            _crawlerRulesRepository = crawlerRulesRepository;
        }

        [Route("{host:string}")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_crawlerRulesRepository.GetRules(host));
        }

        [Route("{id:Guid}")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_crawlerRulesRepository.GetRule(id));
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlRule rule)
        {
            _crawlerRulesRepository.AddRule(rule);
            return Ok();
        }

        [Route("{id:Guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] CrawlRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _crawlerRulesRepository.UpdateRule(rule);
            return Ok();
        }

        [Route("{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _crawlerRulesRepository.RemoveRule(id);
            return Ok();
        }
    }
}