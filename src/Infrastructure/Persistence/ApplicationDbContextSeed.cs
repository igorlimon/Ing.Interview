using Ing.Interview.Domain.Entities;
using Ing.Interview.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Ing.Interview.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(new Account()
                {
                    Currency = Currency.EUR,
                    Iban = "NL69INGB0123456789",
                    Product = "Betaalrekening",
                    ResourceId = "450ffbb8-9f11-4ec6-a1e1-df48aefc82ef",
                    Name = "Hr A van Dijk , Mw B Mol-van Dijk"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
