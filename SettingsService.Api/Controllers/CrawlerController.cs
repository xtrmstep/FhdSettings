using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SettingsService.Api.Models;
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
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller
        /// </summary>
        public CrawlerController(IRulesRepository rulesRepository, IMapper mapper)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof (IList<ExtractRule>))]
        public IHttpActionResult Get()
        {
            return Ok(_rulesRepository.GetDefaultRules());
        }

        /// <summary>
        /// Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(ExtractRule))]
        public IHttpActionResult Get(Guid id)
        {
            var rules = _rulesRepository.GetDefaultRules();
            return Ok(rules.SingleOrDefault(r => r.Id == id));
        }

        /// <summary>
        /// Create a new crawler rule
        /// </summary>
        /// <param name="ruleModel"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] ExtractRuleCreateModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleCreateModel, ExtractRule>(ruleModel);
            var id = _rulesRepository.AddRule(rule);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        /// Update a crawler rule with specified identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="ruleModel">Updated rule object</param>
        /// <returns>Identifiers in the parameter and the object must be equal.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Identifiers do not match</response>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] ExtractRuleUpdateModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleUpdateModel, ExtractRule>(ruleModel);
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