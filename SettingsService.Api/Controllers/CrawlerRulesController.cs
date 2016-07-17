using System;
using System.Web.Http;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    [RoutePrefix("api/crawler/rules")]
    public class CrawlerRulesController : ApiController
    {
        private readonly ICrawlerRuleRepository _crawlerRulesRepository;

        public CrawlerRulesController(ICrawlerRuleRepository crawlerRulesRepository)
        {
            _crawlerRulesRepository = crawlerRulesRepository;
        }

        [Route("")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_crawlerRulesRepository.GetRules(host));
        }

        [Route("{id:guid}")]
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

        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] CrawlRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _crawlerRulesRepository.UpdateRule(rule);
            return Ok();
        }

        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _crawlerRulesRepository.RemoveRule(id);
            return Ok();
        }
    }
}