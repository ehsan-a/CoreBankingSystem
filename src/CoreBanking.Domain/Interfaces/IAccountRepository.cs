using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);
        Account Update(Account account);
        void Delete(Account account);
        Task<Account?> GetByIdAsync(Guid id, ISpecification<Account> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<Account>> GetAllAsync(ISpecification<Account> spec, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Account?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}