using System;
using System.Collections.Generic;
using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface IUrlFrontierSettingsRepository
    {
        /// <summary>
        ///     Get the whole list of URLs in the seed
        /// </summary>
        /// <returns></returns>
        IList<CrawlUrlSeed> GetSeedUrls();

        /// <summary>
        ///     Remove existing URL from the seed
        /// </summary>
        /// <param name="id"></param>
        void RemoveSeedUrl(Guid id);

        /// <summary>
        ///     Add new URL to the seed
        /// </summary>
        /// <param name="url"></param>
        Guid AddSeedUrl(string url);

        /// <summary>
        /// Get URL by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CrawlUrlSeed GetUrl(Guid id);
    }
}