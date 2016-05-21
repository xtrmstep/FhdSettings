using System;
using System.Collections.Generic;
using FhdSettings.Data.Models;

namespace FhdSettings.Data
{
    public interface ICrawlerRepository
    {
        #region rules

        /// <summary>
        ///     Returns a single rule
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        CrawlRule GetRule(Guid ruleId);

        /// <summary>
        ///     Returns a list of rules specific for the host
        /// </summary>
        /// <returns></returns>
        IList<CrawlRule> GetRules(string host);

        /// <summary>
        ///     Update existing rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        void UpdateRule(CrawlRule rule);

        /// <summary>
        ///     Add a new rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        void AddRule(CrawlRule rule);

        /// <summary>
        ///     Remove existing rule
        /// </summary>
        /// <param name="ruleId"></param>
        void RemoveRule(Guid ruleId);

        #endregion

        #region hosts

        /// <summary>
        ///     Get a query to get settings for a host of the URL
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        CrawlHostSetting GetHostSettings(string host);

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

        #endregion

        #region seed

        /// <summary>
        ///     Get the whole list of urls in the seed
        /// </summary>
        /// <returns></returns>
        IList<string> GetSeedUrls();

        /// <summary>
        ///     Remove existing URL from the seed
        /// </summary>
        /// <param name="url"></param>
        void RemoveSeedUrl(string url);

        /// <summary>
        ///     Add new URL to the seed
        /// </summary>
        /// <param name="url"></param>
        void AddSeedUrl(string url);

        #endregion
    }
}