using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreBankingContext _context;

        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Account> Accounts { get; }
        public IGenericRepository<Transaction> Transactions { get; }

        public UnitOfWork(CoreBankingContext context)
        {
            _context = context;

            Customers = new GenericRepository<Customer>(_context);
            Accounts = new GenericRepository<Account>(_context);
            Transactions = new GenericRepository<Transaction>(_context);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
