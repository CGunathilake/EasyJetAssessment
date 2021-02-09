using System;
using System.Collections.Generic;
using System.Linq;
using EasyJetAssessment.Models;

namespace EasyJetAssessment.Services
{
    public class MoneyCalculator : IMoneyCalculator
    {
        public IMoney Max(IEnumerable<IMoney> monies)
        {
            var moniesList = monies.ToList();
            
            if (moniesList.Any(x => x.Currency != moniesList.First().Currency))
            {
                throw new ArgumentException("All monies are not in the same currency");
            }

            return moniesList.OrderByDescending(x => x.Amount).First();
        }

        public IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies)
        {
            return monies.GroupBy(x => x.Currency).Select(x =>
            {
                return new Money(x.Sum(y => y.Amount), x.Key);
            });
        }
    }
}
