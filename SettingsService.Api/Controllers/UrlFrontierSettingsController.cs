using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using SettingsService.Core.Data;
using SettingsService.Core.Data.Models;

namespace SettingsService.Api.Controllers
{
    /// <summary>
    ///     Provides methods to manipulate with url frontier seed
    /// </summary>
    [RoutePrefix("api/urls")]
    public class UrlFrontierSettingsController : ApiController
    {
        private readonly IUrlFrontierSettingsRepository _urlFrontierSettingsRepository;

        /// <summary>
        ///     Controller
        /// </summary>
        /// <param name="urlFrontierSettingsRepository"></param>
        public UrlFrontierSettingsController(IUrlFrontierSettingsRepository urlFrontierSettingsRepository)
        {
            _urlFrontierSettingsRepository = urlFrontierSettingsRepository;
        }

        /// <summary>
        ///     Get all URLs from the frontier seed
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof (IList<CrawlUrlSeed>))]
        public IHttpActionResult Get()
        {
            return Ok(_urlFrontierSettingsRepository.GetSeedUrls());
        }

        /// <summary>
        ///     Get URL by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (CrawlUrlSeed))]
        public IHttpActionResult Get(Guid id)
        {
            var crawlUrlSeed = _urlFrontierSettingsRepository.GetUrl(id);
            return Ok(crawlUrlSeed);
        }

        /// <summary>
        ///     Add new URL to the frontier seed
        /// </summary>
        /// <param name="crawlUrlSeed">URL to be added</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] CrawlUrlSeed crawlUrlSeed)
        {
            var url = crawlUrlSeed.Url;
            var id = _urlFrontierSettingsRepository.AddSeedUrl(url);
            var location = new Uri(Request.RequestUri + "/" + id);
            return Created(location, id);
        }

        /// <summary>
        ///     Delete URL from the frontier seed
        /// </summary>
        /// <param name="id">Guid identifier of the seed</param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            _urlFrontierSettingsRepository.RemoveSeedUrl(id);
            return Ok();
        }
    }
}