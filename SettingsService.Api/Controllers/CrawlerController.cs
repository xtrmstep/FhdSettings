using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SettingsService.Api.Models;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    /// <summary>
    ///     Provides methods to manipulate with crawler rules
    /// </summary>
    [RoutePrefix("api/rules")]
    public class CrawlerController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IRulesRepository _rulesRepository;

        /// <summary>
        ///     Controller
        /// </summary>
        public CrawlerController(IRulesRepository rulesRepository, IMapper mapper)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof (IList<ExtractRule>))]
        public IHttpActionResult Get()
        {
            return Ok(_rulesRepository.Get());
        }

        /// <summary>
        ///     Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (ExtractRule))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_rulesRepository.Get(id));
        }

        /// <summary>
        ///     Create a new crawler rule
        /// </summary>
        /// <param name="ruleModel"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] ExtractRuleCreateModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleCreateModel, ExtractRule>(ruleModel);
            var id = _rulesRepository.Add(rule);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        ///     Update a crawler rule with specified identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="ruleModel">Updated rule object</param>
        /// <returns>Identifiers in the parameter and the object must be equal.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Identifiers do not match</response>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] ExtractRuleCreateModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleCreateModel, ExtractRule>(ruleModel);
            rule.Id = id;
            _rulesRepository.Update(rule);
            return Ok();
        }

        /// <summary>
        ///     Delete a crawler rule with specified identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _rulesRepository.Remove(id);
            return Ok();
        }
    }
}