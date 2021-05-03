using Ing.Interview.Application.Common.Interfaces;
using Ing.Interview.Domain.Entities;

namespace Ing.Interview.Infrastructure._3rdPartyApi.Ing
{
    internal class IngPsd2Api : IApplicationDbContext
    {
        public IStorage<Account> Accounts { get; }
        public IStorage<Transaction> Transactions { get; }
    }
}
