using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByIdAsync(Guid id, ISpecification<Customer> spec, CancellationToken cancellationToken = default);
        Task<Customer?> GetByNationalCodeAsync(string nationalCode, ISpecification<Customer> spec, CancellationToken cancellationToken = default);
        Task<IEnumerable<Customer>> GetAllAsync(ISpecification<Customer> spec, CancellationToken cancellationToken = default);
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Customer Update(Customer customer);
        void Delete(Customer customer);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<Customer?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    }
}