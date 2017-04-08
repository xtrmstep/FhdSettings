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
    public class RulesController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IRulesRepository _rulesRepository;

        /// <summary>
        ///     Controller
        /// </summary>
        public RulesController(IRulesRepository rulesRepository, IMapper mapper)
        {
            _rulesRepository = rulesRepository;
            _mapper = mapper;
        }

        /// <summary>
        ///     Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof (IList<ExtractRuleReadModel>))]
        public IHttpActionResult Get()
        {
            var extractRules = _rulesRepository.Get();
            var viewModel = _mapper.Map<IList<ExtractRuleReadModel>>(extractRules);
            return Ok(viewModel);
        }

        /// <summary>
        ///     Get a list of extract rules
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (ExtractRuleReadModel))]
        public IHttpActionResult Get(Guid id)
        {
            var extractRule = _rulesRepository.Get(id);
            var viewModel = _mapper.Map<ExtractRuleReadModel>(extractRule);
            return Ok(viewModel);
        }

        /// <summary>
        ///     Create a new crawler rule
        /// </summary>
        /// <param name="ruleModel"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] ExtractRuleModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleModel, ExtractRule>(ruleModel);
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
        public IHttpActionResult Put(Guid id, [FromBody] ExtractRuleModel ruleModel)
        {
            var rule = _mapper.Map<ExtractRuleModel, ExtractRule>(ruleModel);
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