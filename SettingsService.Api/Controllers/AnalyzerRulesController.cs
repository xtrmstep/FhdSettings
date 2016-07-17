using System;
using System.Web.Http;
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

        [Route("")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_analizerRepository.GetNumericRules(host));
        }

        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_analizerRepository.GetNumericRule(id));
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] NumericDataExtractorRule rule)
        {
            _analizerRepository.AddNumericRule(rule);
            return Ok();
        }

        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] NumericDataExtractorRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _analizerRepository.UpdateNumericRule(rule);
            return Ok();
        }

        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _analizerRepository.RemoveNumericRule(id);
            return Ok();
        }
    }
}