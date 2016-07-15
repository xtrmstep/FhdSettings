using System;
using System.Web.Http;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Controllers
{
    [RoutePrefix("api/analyzer/rules")]
    public class AnalyzerRulesController : ApiController
    {
        private readonly IAnalizerRepository _analizerRepository;

        public AnalyzerRulesController(IAnalizerRepository analizerRepository)
        {
            _analizerRepository = analizerRepository;
        }

        [Route("{host:string}")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_analizerRepository.GetNumericRules(host));
        }

        [Route("{id:Guid}")]
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

        [Route("{id:Guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] NumericDataExtractorRule rule)
        {
            if (id != rule.Id) return BadRequest();
            _analizerRepository.UpdateNumericRule(rule);
            return Ok();
        }

        [Route("{id:Guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _analizerRepository.RemoveNumericRule(id);
            return Ok();
        }
    }
}