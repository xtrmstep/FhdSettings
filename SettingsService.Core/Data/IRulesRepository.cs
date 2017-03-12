using System;
using System.Collections.Generic;
using SettingsService.Core.Data.Models;

namespace SettingsService.Core.Data
{
    public interface IRulesRepository
    {
        /// <summary>
        ///     Returns a single rule
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        ExtractRule GetRule(Guid ruleId);

        /// <summary>
        ///     Update existing rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        void UpdateRule(ExtractRule rule);

        /// <summary>
        ///     Add a new rule
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        Guid AddRule(ExtractRule rule);

        /// <summary>
        ///     Remove existing rule
        /// </summary>
        /// <param name="id"></param>
        void RemoveRule(Guid id);

        /// <summary>
        /// Returns a list of rules specific for the host
        /// </summary>
        /// <param name="host">Host name or empty string for default rules</param>
        /// <returns></returns>
        IList<ExtractRule> GetRules(string host);
    }
}