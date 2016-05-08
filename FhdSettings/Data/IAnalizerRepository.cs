using System;
using System.Collections.Generic;
using FhdSettings.Data.Models;

namespace FhdSettings.Data
{
    public interface IAnalizerRepository
    {
        /// <summary>
        ///     Returns a list of rules to extract numeric data for a specific URL
        /// </summary>
        /// <returns></returns>
        IList<NumericDataExtractorRule> GetNumericRules(string url);

        /// <summary>
        ///     Update existing rule
        /// </summary>
        /// <param name="rule"></param>
        void UpdateNumericRule(NumericDataExtractorRule rule);

        /// <summary>
        ///     Remove existing rule
        /// </summary>
        /// <param name="id"></param>
        void RemoveNumericRule(Guid id);

        /// <summary>
        ///     Add a new rule
        /// </summary>
        /// <param name="rule"></param>
        void AddNumericRule(NumericDataExtractorRule rule);
    }
}