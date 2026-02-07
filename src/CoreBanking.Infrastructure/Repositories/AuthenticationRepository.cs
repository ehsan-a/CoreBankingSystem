using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Infrastructure.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly CoreBankingContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public AuthenticationRepository(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task<Authentication> AddAsync(Authentication authentication, CancellationToken cancellationToken = default)
        {
            return (await _context.Authentications.AddAsync(authentication, cancellationToken)).Entity;
        }

        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _context.Authentications
                        .AsNoTracking()
                        .AnyAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public async Task<Authentication?> GetByNationalCodeAsync(string nationalCode, ISpecification<Authentication> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Authentications.AsQueryable();
            queryable = SpecificationEvaluator<Authentication>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }
    }
}
