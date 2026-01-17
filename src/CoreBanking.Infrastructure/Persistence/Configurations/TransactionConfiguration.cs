using CoreBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Infrastructure.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.DebitAccount)
             .WithMany(a => a.DebitTransactions)
             .HasForeignKey(t => t.DebitAccountId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.CreditAccount)
               .WithMany(a => a.CreditTransactions)
               .HasForeignKey(t => t.CreditAccountId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Amount)
            .HasPrecision(18, 2);
        }
    }
}
