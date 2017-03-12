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
    public class CrawlerController : ApiController
    {
        private readonly IRulesRepository _rulesRepository;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="rulesRepository"></param>
        public CrawlerController(IRulesRepository rulesRepository)
        {
            _rulesRepository = rulesRepository;
        }

        /// <summary>
        /// Get a crawler rule by identifier
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<ExtractRule>))]
        public IHttpActionResult Get([FromUri]string host)
        {
            return Ok(_rulesRepository.GetRules(host));
        }

        /// <summary>
        /// Get a crawler rule by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (ExtractRule))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_rulesRepository.GetRule(id));
        }

        /// <summary>
        /// Get host default settings
        /// </summary>
        /// <returns></returns>
        [Route("default")]
        [ResponseType(typeof(IList<ExtractRule>))]
        public IHttpActionResult GetDefault()
        {
            return Ok(_rulesRepository.GetRules(string.Empty));
        }

        /// <summary>
        /// Create a new crawler rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] ExtractRule rule)
        {
            var id = _rulesRepository.AddRule(rule);
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
        public IHttpActionResult Put(Guid id, [FromBody] ExtractRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _rulesRepository.UpdateRule(rule);
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
            _rulesRepository.RemoveRule(id);
            return Ok();
        }
    }
}