using FluentAssertions;
using Ing.Interview.Domain.Exceptions;
using Ing.Interview.Domain.ValueObjects;
using NUnit.Framework;

namespace Ing.Interview.Domain.UnitTests.ValueObjects
{
   public class CurrencyTests
    {
        [Test]
        public void ShouldHaveCorrectCurrencyCode()
        {
            const string currencyString = "USD";

            var currency = Currency.From(currencyString);

            currency.Code.Should().Be("USD");
        }

        [Test]
        public void ToStringReturnsCorrectFormat()
        {
            const string currencyString = "USD";

            var currency = Currency.From(currencyString);

            var result = currency.ToString();

            result.Should().Be(currencyString);
        }

        [Test]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string currencyString = "USD";

            var currency = Currency.From(currencyString);

            string result = currency;

            result.Should().Be(currencyString);
        }

        [Test]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string currencyString = "USD";

            var currency = (Currency)currencyString;

            currency.Code.Should().Be("USD");
        }

        [Test]
        public void ShouldThrowUnsupportedCurrencyExceptionForInvalidCurrency()
        {
            FluentActions.Invoking(() => (Currency)"TEST")
                .Should().Throw<UnsupportedCurrencyException>();
        }
    }
}
