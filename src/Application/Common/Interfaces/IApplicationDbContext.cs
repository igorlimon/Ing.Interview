using Ing.Interview.Domain.Entities;

namespace Ing.Interview.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IStorage<Account> Accounts { get; }

        IStorage<Transaction> Transactions { get; }
    }
}
