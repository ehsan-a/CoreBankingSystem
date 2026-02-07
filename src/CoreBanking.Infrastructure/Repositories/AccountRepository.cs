using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CoreBankingContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public AccountRepository(CoreBankingContext context)
        {
            _context = context;
        }
        public async Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            return (await _context.Accounts.AddAsync(account, cancellationToken)).Entity;
        }

        public Account Update(Account account)
        {
            return _context.Accounts.Update(account).Entity;
        }

        public void Delete(Account account)
        {
            if (account is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.Accounts.Update(account);
            }
            else
            {
                _context.Accounts.Remove(account);
            }
        }
        public async Task<IEnumerable<Account>> GetAllAsync(ISpecification<Account> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Accounts.AsQueryable();
            queryable = SpecificationEvaluator<Account>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<Account?> GetByIdAsync(Guid id, ISpecification<Account> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Accounts.AsQueryable();
            queryable = SpecificationEvaluator<Account>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }
        public async Task<Account?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Accounts
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
    }
}
