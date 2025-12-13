using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Account> Accounts { get; }
        IGenericRepository<Transaction> Transactions { get; }
        IGenericRepository<Authentication> Authentications { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
