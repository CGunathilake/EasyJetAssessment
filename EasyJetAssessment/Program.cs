using System.Collections.Generic;
using EasyJetAssessment.Constants;
using EasyJetAssessment.Models;
using EasyJetAssessment.Services;

namespace EasyJetAssessment
{
    public class Program
    {
        public static void Main()
        {
            IMoney money1 = new Money(10.00m, CurrencyCodes.GBP);
            IMoney money2 = new Money(50.00m, CurrencyCodes.GBP);
            IMoney money3 = new Money(20.00m, CurrencyCodes.GBP);

            IMoney money4 = new Money(75.00m, CurrencyCodes.USD);

            IMoney money5 = new Money(15.00m, CurrencyCodes.EUR);
            IMoney money6 = new Money(5.00m, CurrencyCodes.EUR);

            IMoneyCalculator moneyCalculator = new MoneyCalculator();

            var monies = new List<IMoney> { money1, money2, money3 };
            var maxResult = moneyCalculator.Max(monies);
            
            var moniesWithDifferentCurrencies = new List<IMoney> { money1, money2, money3, money4, money5, money6 };
            var sumPerCurrencyResult = moneyCalculator.SumPerCurrency(moniesWithDifferentCurrencies);
        }
    }
}
