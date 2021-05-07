using System.Collections.Generic;
using System.Linq;
using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.Exceptions;

namespace Ing.Interview.Domain.ValueObjects
{
    public class Currency : ValueObject
    {
        private Currency()
        {
        }

        private Currency(string code)
        {
            Code = code;
        }

        public string Code { get; private init; }

        public static implicit operator string(Currency currency)
        {
            return currency.ToString();
        }

        public static explicit operator Currency(string code)
        {
            return From(code);
        }

        public override string ToString()
        {
            return Code;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }

        public static Currency From(string code)
        {
            var color = new Currency { Code = code };

            if (!SupportedCurrency.Contains(color))
            {
                throw new UnsupportedCurrencyException(code);
            }

            return color;
        }

        protected static IEnumerable<Currency> SupportedCurrency
        {
            get
            {
                yield return EUR;
                yield return USD;
                yield return RON;
            }
        }

        public static Currency EUR => new("EUR");

        public static Currency USD => new("USD");

        public static Currency RON => new("RON");
    }
}