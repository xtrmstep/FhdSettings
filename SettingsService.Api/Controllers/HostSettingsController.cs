using System.Web.Http;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    [RoutePrefix("api/hosts")]
    public class HostSettingsController : ApiController
    {
        private readonly IHostSettingsRepository _hostSettingsRepository;

        public HostSettingsController(IHostSettingsRepository hostSettingsRepository)
        {
            _hostSettingsRepository = hostSettingsRepository;
        }

        [Route("")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_hostSettingsRepository.GetHostSettings(host));
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlHostSetting crawlHostSetting)
        {
            _hostSettingsRepository.AddHostSettings(crawlHostSetting);
            return Ok();
        }

        [Route("{id:guid}")]
        public IHttpActionResult Put(string host, [FromBody] CrawlHostSetting crawlHostSetting)
        {
            if (host != crawlHostSetting.Host) return BadRequest();

            _hostSettingsRepository.UpdateHostSettings(crawlHostSetting);
            return Ok();
        }

        [Route("{id:guid}")]
        public IHttpActionResult Delete(string host)
        {
            _hostSettingsRepository.RemoveHostSettings(host);
            return Ok();
        }
    }
}