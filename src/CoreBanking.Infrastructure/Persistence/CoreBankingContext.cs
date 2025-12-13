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

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Account> Accounts { get; set; } = default!;
        public DbSet<Transaction> Transactions { get; set; } = default!;
        public DbSet<Authentication> Authentications { get; set; } = default!;
    }
}
