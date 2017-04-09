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
    /// Provides methods to manipulate with crawl settings for hosts
    /// </summary>
    [RoutePrefix("api/hosts")]
    public class HostsController : ApiController
    {
        private readonly IHostsRepository _hostsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller
        /// </summary>
        public HostsController(IHostsRepository hostsRepository, IMapper mapper)
        {
            _hostsRepository = hostsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all hosts
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Host>))]
        public IHttpActionResult Get()
        {
            return Ok(_hostsRepository.Get());
        }

        /// <summary>
        /// Add new hostModel
        /// </summary>
        /// <param name="hostModel"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] HostModel hostModel)
        {
            var host = _mapper.Map<HostModel, Host>(hostModel);
            var id = _hostsRepository.Add(host);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        /// Update hostModel crawl settings
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hostModel">Updated crawl settings for the hostModel</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] HostModel hostModel)
        {
            var host = _mapper.Map<HostModel, Host>(hostModel);
            host.Id = id;
            _hostsRepository.Update(host);
            return Ok();
        }

        /// <summary>
        /// Delete crawl settings for specified hostModel
        /// </summary>
        /// <param name="id">Guid identifier of the settings</param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _hostsRepository.Remove(id);
            return Ok();
        }
    }
}