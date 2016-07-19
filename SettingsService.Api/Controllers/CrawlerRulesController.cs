﻿using System;
using System.Web.Http;
using System.Web.Http.Description;
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

        /// <summary>
        /// Get all crawler rules for specific host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Get(string host)
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
        /// Create a new crawler rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlRule rule)
        {
            _crawlerRulesRepository.AddRule(rule);
            return Ok();
        }

        /// <summary>
        /// Update a crawler rule with specified identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="rule">Updated rule object</param>
        /// <returns>Identifiers in the parameter and the object must be equal.</returns>
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