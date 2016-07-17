using System.Web.Http;
using SettingsService.Core.Data;

namespace SettingsService.Api.Controllers
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
        public IHttpActionResult Get()
        {
            return Ok(_urlFrontierSettingsRepository.GetSeedUrls());
        }

        [Route("")]
        public IHttpActionResult Post(string url)
        {
            _urlFrontierSettingsRepository.AddSeedUrl(url);
            return Ok();
        }

        [Route("")]
        public IHttpActionResult Delete(string url)
        {
            _urlFrontierSettingsRepository.RemoveSeedUrl(url);
            return Ok();
        }
    }
}