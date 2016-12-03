using System;
using System.Collections.Generic;
using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface IHostSettingsRepository
    {
        /// <summary>
        /// Get all hosts
        /// </summary>
        /// <returns></returns>
        IList<CrawlHostSetting> GetHostSettings();

        /// <summary>
        ///     Get a query to get settings for a host of the URL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CrawlHostSetting GetHostSettings(Guid id);

        /// <summary>
        ///     Update existing host settings
        /// </summary>
        /// <param name="hostSettings"></param>
        /// <returns></returns>
        void UpdateHostSettings(CrawlHostSetting hostSettings);

        /// <summary>
        ///     Add a new host settings
        /// </summary>
        /// <param name="hostSettings"></param>
        /// <returns></returns>
        void AddHostSettings(CrawlHostSetting hostSettings);

        /// <summary>
        ///     Remove existing host settings
        /// </summary>
        /// <param name="host"></param>
        void RemoveHostSettings(string host);
    }
}