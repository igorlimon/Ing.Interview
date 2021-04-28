using System.Collections.Generic;
using Ing.Interview.Domain.Common;

namespace Ing.Interview.Domain.Entities
{
    public class Transaction : AuditableEntity, IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }
}