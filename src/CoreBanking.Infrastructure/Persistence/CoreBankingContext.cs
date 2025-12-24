using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CoreBanking.Infrastructure.Persistence
{
    public class CoreBankingContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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

            modelBuilder.HasSequence<long>("AccountNumberSequence")
            .StartsAt(1000000000)
            .IncrementsBy(1);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(a => a.AccountNumber)
                      .IsUnique();

                entity.Property(a => a.AccountNumber)
                      .HasMaxLength(20);
            });

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
        public DbSet<User> User { get; set; } = default!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
        public DbSet<IdempotencyKey> IdempotencyKeys { get; set; } = default!;
        public DbSet<AuditLog> AuditLogs { get; set; } = default!;
    }
}
