using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using SettingsService.Core.Data;

namespace SettingsService.Api.Controllers
{
    /// <summary>
    /// Provides methods to manipulate with url frontier seed
    /// </summary>
    [RoutePrefix("api/urls")]
    public class UrlFrontierSettingsController : ApiController
    {
        private readonly IUrlFrontierSettingsRepository _urlFrontierSettingsRepository;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="urlFrontierSettingsRepository"></param>
        public UrlFrontierSettingsController(IUrlFrontierSettingsRepository urlFrontierSettingsRepository)
        {
            _urlFrontierSettingsRepository = urlFrontierSettingsRepository;
        }

        /// <summary>
        /// Get all URLs from the frontier seed
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof(IList<string>))]
        public IHttpActionResult Get()
        {
            return Ok(_urlFrontierSettingsRepository.GetSeedUrls());
        }

        /// <summary>
        /// Add new URL to the frontier seed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post(string url)
        {
            _urlFrontierSettingsRepository.AddSeedUrl(url);
            return Ok();
        }

        /// <summary>
        /// Delete URL from the frontier seed
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Delete(string url)
        {
            _urlFrontierSettingsRepository.RemoveSeedUrl(url);
            return Ok();
        }
    }
}