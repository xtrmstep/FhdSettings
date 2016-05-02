using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FhdSettings.Data;
using FhdSettings.Data.Models;

namespace FhdSettings.Impl.Repositories
{
    class AnalizerRepository : IAnalizerRepository
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
