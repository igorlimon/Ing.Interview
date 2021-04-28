using System.Collections.Generic;
using System.Linq;
using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.Exceptions;

namespace Ing.Interview.Domain.ValueObjects
{
    public class Currency : ValueObject
    {
        static Currency()
        {
        }

        private Currency()
        {
        }

        private Currency(string code)
        {
            Code = code;
        }

        public static Currency From(string code)
        {
            var colour = new Currency { Code = code };

            if (!SupportedCurrency.Contains(colour))
            {
                throw new UnsupportedColourException(code);
            }

            return colour;
        }

        public static Currency EUR => new Currency("EUR");

        public static Currency USD => new Currency("USD");

        public static Currency RON => new Currency("RON");

        public string Code { get; private set; }

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

        protected static IEnumerable<Currency> SupportedCurrency
        {
            get
            {
                yield return EUR;
                yield return USD;
                yield return RON;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}