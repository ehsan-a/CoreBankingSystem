using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction?> GetByIdAsync(Guid id, ISpecification<Transaction> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<Transaction>> GetAllAsync(ISpecification<Transaction> spec, CancellationToken cancellationToken = default);
        Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    }
}