using System;
using System.Collections.Generic;
using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.ValueObjects;

namespace Ing.Interview.Domain.Entities
{
    public class Account : AuditableEntity, IHasDomainEvent
    {
        public Account()
        {
            DomainEvents = new List<DomainEvent>();
        }

        public Guid AccountId { get; set; }
        public string ResourceId { get; set; }
        public string Product { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
