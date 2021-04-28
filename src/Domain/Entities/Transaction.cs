using System;
using System.Collections.Generic;
using Ing.Interview.Domain.Common;

namespace Ing.Interview.Domain.Entities
{
    public class Transaction : AuditableEntity, IHasDomainEvent
    {
        public string Iban { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }
    }
}