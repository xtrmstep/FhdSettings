using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using SettingsService.Api.Models;
using SettingsService.Api.Types;
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
        /// <param name="hostsRepository"></param>
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
            return Ok(_hostsRepository.GetHosts());
        }

        /// <summary>
        /// Add new hostCreateModel
        /// </summary>
        /// <param name="hostCreateModel"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] HostCreateModel hostCreateModel)
        {
            var host = _mapper.Map<HostCreateModel, Host>(hostCreateModel);
            var id = _hostsRepository.AddHost(host);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        /// Update hostCreateModel crawl settings
        /// </summary>
        /// <param name="hostSetting">Updated crawl settings for the hostCreateModel</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] Host hostSetting)
        {
            if (id == hostSetting.Id)
                _hostsRepository.UpdateHost(hostSetting);

            return Ok();
        }

        /// <summary>
        /// Delete crawl settings for specified hostCreateModel
        /// </summary>
        /// <param name="id">Guid identifier of the settings</param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _hostsRepository.RemoveHost(id);
            return Ok();
        }
    }
}