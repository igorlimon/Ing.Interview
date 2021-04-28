using Ing.Interview.Application.Common.Interfaces;
using System;

namespace Ing.Interview.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
