using Ing.Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ing.Interview.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        IStorage<Account> Accounts { get; }

        IStorage<Transaction> Transactions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
