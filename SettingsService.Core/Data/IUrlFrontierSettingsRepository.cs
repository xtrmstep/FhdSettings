using System.Collections.Generic;

namespace SettingsService.Core.Data
{
    public interface IUrlFrontierSettingsRepository
    {
        #region hosts

        #endregion

        #region seed

        /// <summary>
        ///     Get the whole list of URLs in the seed
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