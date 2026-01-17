using CoreBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Infrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            var debitTransactionsNavigation = builder.Metadata.FindNavigation(nameof(Account.DebitTransactions));

            debitTransactionsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            var creditTransactionsNavigation = builder.Metadata.FindNavigation(nameof(Account.CreditTransactions));

            creditTransactionsNavigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.HasIndex(a => a.AccountNumber)
                  .IsUnique();

            builder.Property(a => a.AccountNumber)
                  .HasMaxLength(20);

            builder.Property(t => t.Balance)
                .HasPrecision(18, 2);
        }
    }
}