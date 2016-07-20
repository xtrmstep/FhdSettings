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
        /// Get host settings with crawl delay and other
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(CrawlHostSetting))]
        public IHttpActionResult Get(string host)
        {
            return Ok(_hostSettingsRepository.GetHostSettings(host));
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
        /// <param name="host">Host name</param>
        /// <param name="crawlHostSetting">Updated crawl settings for the host</param>
        /// <returns>Host in the parameter and in the object must be equal.</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Identifiers do not match</response>
        [Route("{id:guid}")]
        public IHttpActionResult Put(string host, [FromBody] CrawlHostSetting crawlHostSetting)
        {
            if (host != crawlHostSetting.Host) return BadRequest();

            _hostSettingsRepository.UpdateHostSettings(crawlHostSetting);
            return Ok();
        }

        /// <summary>
        /// Delete crawl settings for specified host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(string host)
        {
            _hostSettingsRepository.RemoveHostSettings(host);
            return Ok();
        }
    }
}