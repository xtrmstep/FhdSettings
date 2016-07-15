using System.Web.Http;
using FhdSettings.Data;

namespace FhdSettings.Api.Controllers
{
    [RoutePrefix("api/urls")]
    public class UrlFrontierSettingsController : ApiController
    {
        private readonly IUrlFrontierSettingsRepository _urlFrontierSettingsRepository;

        public UrlFrontierSettingsController(IUrlFrontierSettingsRepository urlFrontierSettingsRepository)
        {
            _urlFrontierSettingsRepository = urlFrontierSettingsRepository;
        }

        [Route("")]
        public IHttpActionResult Get(string host)
        {
            return Ok(_urlFrontierSettingsRepository.GetSeedUrls());
        }

        [Route("")]
        public IHttpActionResult Post([FromBody] string url)
        {
            _urlFrontierSettingsRepository.AddSeedUrl(url);
            return Ok();
        }

        [Route("{url:string}")]
        public IHttpActionResult Delete(string url)
        {
            _urlFrontierSettingsRepository.RemoveSeedUrl(url);
            return Ok();
        }
    }
}