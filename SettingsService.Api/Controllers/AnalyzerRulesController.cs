using System;
using System.Web.Http;
using System.Web.Http.Description;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    [RoutePrefix("api/analyzer/rules")]
    public class AnalyzerRulesController : ApiController
    {
        private readonly IAnalizerRepository _analizerRepository;

        public AnalyzerRulesController(IAnalizerRepository analizerRepository)
        {
            _analizerRepository = analizerRepository;
        }

        /// <summary>
        /// Get analyzer rules for the host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_analizerRepository.GetNumericRules(host));
        }

        /// <summary>
        /// Get an analyzer rule with specified identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(NumericDataExtractorRule))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_analizerRepository.GetNumericRule(id));
        }

        /// <summary>
        /// Create a new analyzer rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] NumericDataExtractorRule rule)
        {
            _analizerRepository.AddNumericRule(rule);
            return Ok();
        }

        /// <summary>
        /// Update an analyzer rule with specified identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rule">Updated analyzer rule</param>
        /// <returns>Identifiers in the parameter and the rule object must be equal.</returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] NumericDataExtractorRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _analizerRepository.UpdateNumericRule(rule);
            return Ok();
        }

        /// <summary>
        /// Delete an analyzer rule
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _analizerRepository.RemoveNumericRule(id);
            return Ok();
        }
    }
}