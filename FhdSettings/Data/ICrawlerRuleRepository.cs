using System;
using System.Collections.Generic;
using FhdSettings.Data.Models;

namespace FhdSettings.Data
{
    public interface ICrawlerRuleRepository
    {
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
    }
}