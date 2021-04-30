using System;

namespace Ing.Interview.Domain.Exceptions
{
    public class UnsupportedCurrencyException : Exception
    {
        public UnsupportedCurrencyException(string code)
            : base($"Currency \"{code}\" is unsupported.")
        {
        }
    }
}
