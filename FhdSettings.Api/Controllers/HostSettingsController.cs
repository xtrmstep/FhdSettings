using System;
using System.Web.Http;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Controllers
{
    [RoutePrefix("api/hosts")]
    public class HostSettingsController : ApiController
    {
        private readonly IHostSettingsRepository _hostSettingsRepository;

        public HostSettingsController(IHostSettingsRepository hostSettingsRepository)
        {
            _hostSettingsRepository = hostSettingsRepository;
        }

        [Route("{host:string}")]
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

        [Route("{id:Guid}")]
        public IHttpActionResult Put(Guid id, [FromBody] CrawlHostSetting crawlHostSetting)
        {
            if (id != crawlHostSetting.Id) return BadRequest();

            _hostSettingsRepository.UpdateHostSettings(crawlHostSetting);
            return Ok();
        }
    }
}