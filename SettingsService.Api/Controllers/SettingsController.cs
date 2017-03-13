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
    [RoutePrefix("api/settings")]
    public class SettingsController : ApiController
    {
        private readonly ISettingsRepository _hostSettingsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="hostSettingsRepository"></param>
        /// <param name="mapper"></param>
        public SettingsController(ISettingsRepository hostSettingsRepository, IMapper mapper)
        {
            _hostSettingsRepository = hostSettingsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Return list of all settings
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<Setting>))]
        public IHttpActionResult Get()
        {
            return Ok(_hostSettingsRepository.Get());
        }

        /// <summary>
        /// Get host settings with crawl delay and other
        /// </summary>
        /// <param name="id">Guid identifier of settings</param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(Setting))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_hostSettingsRepository.Get(id));
        }

        /// <summary>
        /// Create a record with settings for a new host
        /// </summary>
        /// <param name="settingModel">Host create model</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] SettingModel settingModel)
        {
            var hostSettings = _mapper.Map<SettingModel, Setting>(settingModel);
            var newId = _hostSettingsRepository.Add(hostSettings);
            var location = new Uri(Request.RequestUri + "/" + newId);
            return Created(location, newId);
        }

        /// <summary>
        /// Update host crawl settings
        /// </summary>
        /// <param name="id"></param>
        /// <param name="settingModel">Updated crawl settings for the host</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        [Route("{id:guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] SettingModel settingModel)
        {
            var hostSettings = _mapper.Map<SettingModel, Setting>(settingModel);
            hostSettings.Id = id;
            _hostSettingsRepository.Update(hostSettings);
            return Ok();
        }

        /// <summary>
        /// Delete crawl settings for specified host
        /// </summary>
        /// <param name="id">Guid identifier of the settings</param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _hostSettingsRepository.Remove(id);
            return Ok();
        }
    }
}