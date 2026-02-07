using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CoreBankingContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public TransactionRepository(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            return (await _context.Transactions.AddAsync(transaction, cancellationToken)).Entity;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(ISpecification<Transaction> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Transactions.AsQueryable();
            queryable = SpecificationEvaluator<Transaction>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, ISpecification<Transaction> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Transactions.AsQueryable();
            queryable = SpecificationEvaluator<Transaction>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
