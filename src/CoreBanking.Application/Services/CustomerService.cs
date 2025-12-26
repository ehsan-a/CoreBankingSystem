using AutoMapper;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace CoreBanking.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, UserManager<User> userManager, IAuditLogService auditLogService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task CreateAsync(Customer customer, ClaimsPrincipal principal, CancellationToken cancellationToken)
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

            var user = await _userManager.GetUserAsync(principal);
            Customer.Create(customer, user.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var item = await _unitOfWork.Customers.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;

            var user = await _userManager.GetUserAsync(principal);
            Customer.Delete(item, user.Id);

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

        public async Task UpdateAsync(Customer customer, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {

            var oldAccount = await _unitOfWork.Customers
                .GetByIdAsNoTrackingAsync(customer.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _unitOfWork.Customers.Update(customer);

            var user = await _userManager.GetUserAsync(principal);
            Customer.Update(customer, user.Id, oldValue);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            return await _unitOfWork.Customers.ExistsByIdAsync(id, spec, cancellationToken);
        }
    }
}
