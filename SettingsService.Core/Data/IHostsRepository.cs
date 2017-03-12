using System;
using System.Collections.Generic;
using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface IHostsRepository
    {
        /// <summary>
        ///     Get the whole list of URLs in the seed
        /// </summary>
        /// <returns></returns>
        IList<Host> GetHosts();

        /// <summary>
        ///     Remove existing URL from the seed
        /// </summary>
        /// <param name="id"></param>
        void RemoveHost(Guid id);

        /// <summary>
        ///     Add new URL to the seed
        /// </summary>
        /// <param name="host"></param>
        Guid AddHost(Host host);

        /// <summary>
        ///     Update existing host
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        void UpdateHost(Host host);
    }
}