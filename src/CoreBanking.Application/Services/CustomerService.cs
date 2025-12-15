using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;

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
            var spec = new AuthenticationGetAllSpec();
            var authenticationResult = await _unitOfWork.Authentications.GetByNationalCodeAsync(customer.NationalCode, spec, cancellationToken);
            if (await _unitOfWork.Customers.ExistsByNationalCodeAsync(customer.NationalCode, cancellationToken))
                throw new ConflictException("Customer already exists.");
            if (authenticationResult == null)
                throw new UnauthorizedAccessException("Customer Authentication Not Found.");
            if (!authenticationResult.CentralBankCreditCheckPassed || !authenticationResult.CivilRegistryVerified || !authenticationResult.PoliceClearancePassed)
                throw new UnauthorizedAccessException("Customer authentication result rejected.");
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
