using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CustomerResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            var spec = new AuthenticationGetAllSpec();
            var authenticationResult = await _unitOfWork.Authentications.GetByNationalCodeAsync(customer.NationalCode, spec, cancellationToken);
            if (await _unitOfWork.Customers.ExistsByNationalCodeAsync(customer.NationalCode, cancellationToken))
                throw new ConflictException("Customer already exists.");
            if (authenticationResult == null)
                throw new UnauthorizedAccessException("Customer Authentication Not Found.");
            if (!authenticationResult.CentralBankCreditCheckPassed || !authenticationResult.CivilRegistryVerified || !authenticationResult.PoliceClearancePassed)
                throw new UnauthorizedAccessException("Customer authentication result rejected.");
            await _unitOfWork.Customers.AddAsync(customer, cancellationToken);

            Customer.Create(customer, request.UserId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CustomerResponseDto>(customer);
        }
    }
}
