using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _unitOfWork.Customers.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var item = await _unitOfWork.Customers.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Customers.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task<Customer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.GetByNationalCodeAsync(nationalCode, spec, cancellationToken);
        }

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.ExistsByIdAsync(id, spec, cancellationToken);
        }

    }
}
