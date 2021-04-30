using Ing.Interview.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ing.Interview.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.Amount).HasColumnType("DECIMAL(5,2)");
            builder.ToTable(nameof(Transaction));
        }
    }
}