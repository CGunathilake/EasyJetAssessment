using System;
using System.Collections.Generic;
using System.Linq;
using EasyJetAssessment.Constants;
using EasyJetAssessment.Models;
using EasyJetAssessment.Services;
using NUnit.Framework;

namespace EasyJetAssessment.Tests
{
    [TestFixture]
    public class MoneyCalculatorTests
    {
        private readonly IMoney _moneyGbp10 = new Money(10.00m, CurrencyCodes.GBP);
        private readonly IMoney _moneyGbp20 = new Money(20.00m, CurrencyCodes.GBP);
        private readonly IMoney _moneyGbp50 = new Money(50.00m, CurrencyCodes.GBP);
        
        private readonly IMoney _moneyUsd20 = new Money(20.00m, CurrencyCodes.USD);
        
        private readonly IMoney _moneyEur50 = new Money(50.00m, CurrencyCodes.EUR);
        private readonly IMoney _moneyEur15 = new Money(15.00m, CurrencyCodes.EUR);

        private MoneyCalculator _moneyCalculator;

        [SetUp]
        public void SetUp()
        {
            _moneyCalculator = new MoneyCalculator();
        }

        [Test]
        public void Max_ThrowsAnArgumentException_WhenAllMoniesAreNotInTheSameCurrency()
        {
            // Arrange
            var monies = new List<IMoney> { _moneyGbp10, _moneyUsd20 };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _moneyCalculator.Max(monies));

            // Assert
            Assert.That("All monies are not in the same currency", Is.EqualTo(exception?.Message));
        }

        [Test]
        public void Max_ReturnsTheLargestAmountOfMoney()
        {
            // Arrange
            var monies = new List<IMoney> { _moneyGbp10, _moneyGbp20, _moneyGbp50 };
            
            // Act
            var money = _moneyCalculator.Max(monies);

            // Assert
            Assert.That(_moneyGbp50, Is.EqualTo(money));
        }

        [Test]
        public void Max_ThrowsAnArgumentNullException_WhenMoniesAreNull()
        {
            // Act
            void Func() => _moneyCalculator.Max(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Func);
        }

        [Test]
        public void Max_ThrowsAnInvalidOperationException_WhenMoniesAreEmpty()
        {
            // Act
            void Func() => _moneyCalculator.Max(new List<IMoney>());

            // Assert
            Assert.Throws<InvalidOperationException>(Func);
        }

        [Test]
        public void SumPerCurrency_ReturnAMoneyPerCurrencyWithTheSumOfAllTheMoniesOfTheSameCurrency()
        {
            // Arrange
            var monies = new List<IMoney>{ _moneyGbp10, _moneyGbp20, _moneyGbp50, _moneyUsd20, _moneyEur50, _moneyEur15 };

            var expectedSumOfGbp = _moneyGbp10.Amount + _moneyGbp20.Amount + _moneyGbp50.Amount;
            var expectedSumOfUsd = _moneyUsd20.Amount;
            var expectedSumOfEur = _moneyEur50.Amount + _moneyEur15.Amount;

            // Act
            var sumPerCurrency = _moneyCalculator.SumPerCurrency(monies).ToList();
            
            var resultGbp = sumPerCurrency.Single(x => x.Currency == CurrencyCodes.GBP).Amount;
            var resultUsd = sumPerCurrency.Single(x => x.Currency == CurrencyCodes.USD).Amount;
            var resultEur = sumPerCurrency.Single(x => x.Currency == CurrencyCodes.EUR).Amount;
            
            // Assert
            Assert.That(expectedSumOfGbp, Is.EqualTo(resultGbp));
            Assert.That(expectedSumOfUsd, Is.EqualTo(resultUsd));
            Assert.That(expectedSumOfEur, Is.EqualTo(resultEur));
        }

        [Test]
        public void SumPerCurrency_ThrowsAnArgumentNullException_WhenMoniesAreNull()
        {
            // Act
            void Func() => _moneyCalculator.SumPerCurrency(null);

            // Assert
            Assert.Throws<ArgumentNullException>(Func);
        }

        [Test]
        public void SumPerCurrency_ReturnsAnEmptyListOfMonies_WhenMoniesAreEmpty()
        {
            // Act
            var sumPerCurrency = _moneyCalculator.SumPerCurrency(new List<IMoney>()).ToList();

            // Assert
            Assert.That(sumPerCurrency, Is.Empty);
        }
    }
}