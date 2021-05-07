using System;
using Ing.Interview.Domain.Entities;
using Ing.Interview.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;
using Ing.Interview.Domain.Enums;

namespace Ing.Interview.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(new Account()
                {
                    Currency = Currency.EUR,
                    AccountNumber = "NL69INGB0123456789",
                    Product = "Betaalrekening",
                    ResourceId = "450ffbb8-9f11-4ec6-a1e1-df48aefc82ef",
                    Name = "Hr A van Dijk , Mw B Mol-van Dijk",
                    LastModified = DateTime.UtcNow,
                    LastModifiedBy = "admin"
                });

                await context.SaveChangesAsync();
            }

            if (!context.Transactions.Any())
            {
                context.Transactions.Add(new Transaction()
                {
                    AccountNumber = "NL69INGB0123456789",
                    Amount = 300,
                    CategoryId = (int) TransactionCategory.Clothing,
                    TransactionDate = DateTime.UtcNow,
                    LastModified = DateTime.UtcNow,
                    LastModifiedBy = "admin"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
