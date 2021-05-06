using System;
using System.Collections.Generic;

namespace Ing.Interview.Domain.Common
{
    public interface IHasDomainEvent
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
