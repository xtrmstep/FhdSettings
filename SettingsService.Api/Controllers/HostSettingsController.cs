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
    public class HostSettingsController : ApiController
    {
        private readonly ISettingsRepository _hostSettingsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="hostSettingsRepository"></param>
        /// <param name="mapper"></param>
        public HostSettingsController(ISettingsRepository hostSettingsRepository, IMapper mapper)
        {
            _hostSettingsRepository = hostSettingsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get host settings with crawl delay and other
        /// </summary>
        /// <param name="id">Guid identifier of settings</param>
        /// <returns></returns>
        [Route("{id:guid}/settings")]
        [ResponseType(typeof(HostSetting))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_hostSettingsRepository.GetHostSettings(id));
        }

        /// <summary>
        /// Create a record with settings for a new host
        /// </summary>
        /// <param name="id">Host identifier</param>
        /// <param name="hostSettingModel">Host create model</param>
        /// <returns></returns>
        [Route("{id:guid}/settings")]
        public IHttpActionResult Post(Guid id, [FromBody] HostSettingCreateModel hostSettingModel)
        {
            var hostSettings = _mapper.Map<HostSettingCreateModel, HostSetting>(hostSettingModel);
            var newId = _hostSettingsRepository.AddHostSettings(hostSettings);
            var location = new Uri(Request.RequestUri + "/" + newId);
            return Created(location, newId);
        }

        /// <summary>
        /// Update host crawl settings
        /// </summary>
        /// <param name="hostId"></param>
        /// <param name="id"></param>
        /// <param name="hostSettingModel">Updated crawl settings for the host</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        [Route("{hostId:guid}/settings/{id:guid}")]
        public IHttpActionResult Put(Guid hostId, Guid id, [FromBody] HostSettingUpdateModel hostSettingModel)
        {
            var hostSettings = _mapper.Map<HostSettingCreateModel, HostSetting>(hostSettingModel);
            if (id == hostSettings.Id)
                _hostSettingsRepository.UpdateHostSettings(hostSettings);

            return Ok();
        }

        /// <summary>
        /// Delete crawl settings for specified host
        /// </summary>
        /// <param name="hostId"></param>
        /// <param name="id">Guid identifier of the settings</param>
        /// <returns></returns>
        [Route("{hostId:guid}/settings/{id:guid}")]
        public IHttpActionResult Delete(Guid hostId, Guid id)
        {
            _hostSettingsRepository.RemoveHostSettings(id);
            return Ok();
        }
    }
}