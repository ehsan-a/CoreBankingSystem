using CoreBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CoreBanking.Infrastructure.Persistence
{
    public class CoreBankingContext : DbContext
    {
        public CoreBankingContext(DbContextOptions options) : base(options)
        {
        }

        protected CoreBankingContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.DebitAccount)
                .WithMany(a => a.DebitTransactions)
                .HasForeignKey(t => t.DebitAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.CreditAccount)
                .WithMany(a => a.CreditTransactions)
                .HasForeignKey(t => t.CreditAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
            .Property(t => t.Balance)
            .HasPrecision(18, 2);

            modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);
        }

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;
        public DbSet<Authentication> Authentications { get; set; } = default!;
    }
}
