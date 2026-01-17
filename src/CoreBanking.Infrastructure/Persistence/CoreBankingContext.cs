using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.HasSequence<long>("AccountNumberSequence")
            .StartsAt(1000000000)
            .IncrementsBy(1);
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
