using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CoreBankingContext _context;
        public GenericRepository(CoreBankingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Set<T>().AsQueryable();
            queryable = SpecificationEvaluator<T>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(Guid id, ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Set<T>().AsQueryable();
            queryable = SpecificationEvaluator<T>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
        }

        public async Task<T> GetByNationalCodeAsync(string nationalCode, ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Set<T>().AsQueryable();
            queryable = SpecificationEvaluator<T>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => EF.Property<string>(e, "NationalCode") == nationalCode, cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.Set<T>().Update(entity);
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public async Task<bool> ExistsByIdAsync(Guid id, ISpecification<T> spec, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, spec, cancellationToken);
            return entity != null;
        }
        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                       .AsNoTracking()
                       .AnyAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
        }
        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, ISpecification<T> spec, CancellationToken cancellationToken)
        {
            var entity = await GetByNationalCodeAsync(nationalCode, spec, cancellationToken);
            return entity != null;
        }

        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                       .AsNoTracking()
                       .AnyAsync(e => EF.Property<string>(e, "NationalCode") == nationalCode, cancellationToken);
        }

        public async Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id, cancellationToken);
        }

    }
}
