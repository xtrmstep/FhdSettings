using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    /// <summary>
    /// Provides methods to manipulate with crawler rules
    /// </summary>
    [RoutePrefix("api/crawler/rules")]
    public class CrawlerRulesController : ApiController
    {
        private readonly ICrawlerRuleRepository _crawlerRulesRepository;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="crawlerRulesRepository"></param>
        public CrawlerRulesController(ICrawlerRuleRepository crawlerRulesRepository)
        {
            _crawlerRulesRepository = crawlerRulesRepository;
        }

        /// <summary>
        /// Get a crawler rule by identifier
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<CrawlRule>))]
        public IHttpActionResult Get([FromUri]string host)
        {
            return Ok(_crawlerRulesRepository.GetRules(host));
        }

        /// <summary>
        /// Get a crawler rule by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (CrawlRule))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_crawlerRulesRepository.GetRule(id));
        }

        /// <summary>
        /// Get host default settings
        /// </summary>
        /// <returns></returns>
        [Route("default")]
        [ResponseType(typeof(IList<CrawlRule>))]
        public IHttpActionResult GetDefault()
        {
            return Ok(_crawlerRulesRepository.GetRules(string.Empty));
        }

        /// <summary>
        /// Create a new crawler rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlRule rule)
        {
            var id = _crawlerRulesRepository.AddRule(rule);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        /// Update a crawler rule with specified identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="rule">Updated rule object</param>
        /// <returns>Identifiers in the parameter and the object must be equal.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Identifiers do not match</response>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] CrawlRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _crawlerRulesRepository.UpdateRule(rule);
            return Ok();
        }

        /// <summary>
        /// Delete a crawler rule with specified identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _crawlerRulesRepository.RemoveRule(id);
            return Ok();
        }
    }
}