using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
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
        private readonly IHostSettingsRepository _hostSettingsRepository;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="hostSettingsRepository"></param>
        public HostSettingsController(IHostSettingsRepository hostSettingsRepository)
        {
            _hostSettingsRepository = hostSettingsRepository;
        }

        /// <summary>
        /// Get settings of all hosts
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<CrawlHostSetting>))]
        public IHttpActionResult Get()
        {
            return Ok(_hostSettingsRepository.GetHostSettings());
        }

        /// <summary>
        /// Get host settings with crawl delay and other
        /// </summary>
        /// <param name="id">Guid identifier of settings</param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof(CrawlHostSetting))]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_hostSettingsRepository.GetHostSettings(id));
        }

        /// <summary>
        /// Get host default settings
        /// </summary>
        /// <returns></returns>
        [Route("default")]
        [ResponseType(typeof(CrawlHostSetting))]
        public IHttpActionResult GetDefault()
        {
            return Ok(_hostSettingsRepository.GetHostSettings(Guid.Empty));
        }

        /// <summary>
        /// Create a record with settings for a new host
        /// </summary>
        /// <param name="crawlHostSetting">Settings object</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlHostSetting crawlHostSetting)
        {
            _hostSettingsRepository.AddHostSettings(crawlHostSetting);
            return Ok();
        }

        /// <summary>
        /// Update host crawl settings
        /// </summary>
        /// <param name="crawlHostSetting">Updated crawl settings for the host</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        [Route("default")]
        public IHttpActionResult Put([FromBody] CrawlHostSetting crawlHostSetting)
        {
            _hostSettingsRepository.UpdateHostSettings(crawlHostSetting);
            return Ok();
        }
        /// <summary>
        /// Update host crawl settings
        /// </summary>
        [Route("default")]
        public IHttpActionResult PutDefault(int delay, string disallow)
        {
            var settings = _hostSettingsRepository.GetHostSettings(Guid.Empty);
            settings.Disallow = disallow;
            settings.CrawlDelay = delay;
            _hostSettingsRepository.UpdateHostSettings(settings);
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
            _hostSettingsRepository.RemoveHostSettings(id);
            return Ok();
        }
    }
}