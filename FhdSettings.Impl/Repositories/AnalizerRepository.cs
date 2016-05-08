using System;
using System.Collections.Generic;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    internal class AnalizerRepository : IAnalizerRepository
    {
        public void AddNumericRule(NumericDataExtractorRule rule)
        {
            throw new NotImplementedException();
        }

        public IList<NumericDataExtractorRule> GetNumericRules(string url)
        {
            throw new NotImplementedException();
        }

        public void RemoveNumericRule(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateNumericRule(NumericDataExtractorRule rule)
        {
            throw new NotImplementedException();
        }
    }
}