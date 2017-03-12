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
        private readonly IHostsRepository _urlFrontierSettingsRepository;

        /// <summary>
        ///     Controller
        /// </summary>
        /// <param name="urlFrontierSettingsRepository"></param>
        public UrlFrontierSettingsController(IHostsRepository urlFrontierSettingsRepository)
        {
            _urlFrontierSettingsRepository = urlFrontierSettingsRepository;
        }

        /// <summary>
        ///     Get all URLs from the frontier seed
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [ResponseType(typeof (IList<Host>))]
        public IHttpActionResult Get()
        {
            return Ok(_urlFrontierSettingsRepository.GetHosts());
        }

        /// <summary>
        ///     Get URL by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [ResponseType(typeof (Host))]
        public IHttpActionResult Get(Guid id)
        {
            var crawlUrlSeed = _urlFrontierSettingsRepository.GetHost(id);
            return Ok(crawlUrlSeed);
        }

        /// <summary>
        ///     Add new URL to the frontier seed
        /// </summary>
        /// <param name="crawlUrlSeed">URL to be added</param>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult Post([FromBody] Host crawlUrlSeed)
        {
            var url = crawlUrlSeed.SeedUrl;
            var id = _urlFrontierSettingsRepository.AddHost(url);
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
            _urlFrontierSettingsRepository.RemoveHost(id);
            return Ok();
        }
    }
}