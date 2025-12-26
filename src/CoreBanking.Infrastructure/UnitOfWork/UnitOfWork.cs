using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreBankingContext _context;
        private readonly IEventDispatcher _eventDispatcher;
        private IDbContextTransaction? _transaction;

        public IGenericRepository<Customer> Customers { get; }
        public IGenericRepository<Account> Accounts { get; }
        public IGenericRepository<Transaction> Transactions { get; }
        public IGenericRepository<Authentication> Authentications { get; }

        public UnitOfWork(CoreBankingContext context, IEventDispatcher eventDispatcher)
        {
            _context = context;
            _eventDispatcher = eventDispatcher;

            Customers = new GenericRepository<Customer>(_context);
            Accounts = new GenericRepository<Account>(_context);
            Transactions = new GenericRepository<Transaction>(_context);
            Authentications = new GenericRepository<Authentication>(_context);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

         public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            if (_transaction != null)
                await _transaction.CommitAsync(cancellationToken);
        }

       
        public async Task RollbackAsync()
        {
            await _transaction!.RollbackAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);

            var entities = _context.ChangeTracker
               .Entries<BaseEntity>()
               .Select(e => e.Entity)
               .Where(e => e.DomainEvents.Any())
               .ToList();

            var events = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in events)
                await _eventDispatcher.Dispatch(domainEvent);
        }
    }
}
