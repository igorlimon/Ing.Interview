using System.Collections.Generic;
using Ing.Interview.Domain.Common;
using Ing.Interview.Domain.ValueObjects;

namespace Ing.Interview.Domain.Entities
{
    public class Account : AuditableEntity, IHasDomainEvent
    {
        public string ResourceId { get; set; }
        public string Product { get; set; }
        public string Iban { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
