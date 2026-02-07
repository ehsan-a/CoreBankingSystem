using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CoreBankingContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public CustomerRepository(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            return (await _context.Customers.AddAsync(customer, cancellationToken)).Entity;
        }

        public void Delete(Customer customer)
        {
            if (customer is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _context.Customers.Update(customer);
            }
            else
            {
                _context.Customers.Remove(customer);
            }
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Customers
                       .AsNoTracking()
                       .AnyAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _context.Customers
                     .AsNoTracking()
                     .AnyAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(ISpecification<Customer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Customers.AsQueryable();
            queryable = SpecificationEvaluator<Customer>.GetQuery(queryable, spec);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Customers
               .AsNoTracking()
               .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, ISpecification<Customer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Customers.AsQueryable();
            queryable = SpecificationEvaluator<Customer>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetByNationalCodeAsync(string nationalCode, ISpecification<Customer> spec, CancellationToken cancellationToken = default)
        {
            var queryable = _context.Customers.AsQueryable();
            queryable = SpecificationEvaluator<Customer>.GetQuery(queryable, spec);
            return await queryable.FirstOrDefaultAsync(e => e.NationalCode == nationalCode, cancellationToken);
        }

        public Customer Update(Customer customer)
        {
            return _context.Customers.Update(customer).Entity;
        }
    }
}
