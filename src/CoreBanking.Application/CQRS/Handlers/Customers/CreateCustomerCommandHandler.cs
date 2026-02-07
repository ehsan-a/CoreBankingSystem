using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Authentications;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, CustomerResponseDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IAuthenticationRepository authenticationRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            var spec = new AuthenticationGetAllSpec();
            var authenticationResult = await _authenticationRepository.GetByNationalCodeAsync(customer.NationalCode, spec, cancellationToken);
            if (await _customerRepository.ExistsByNationalCodeAsync(customer.NationalCode, cancellationToken))
                throw new ConflictException("Customer already exists.");
            if (authenticationResult == null)
                throw new UnauthorizedAccessException("Customer Authentication Not Found.");
            if (!authenticationResult.CentralBankCreditCheckPassed || !authenticationResult.CivilRegistryVerified || !authenticationResult.PoliceClearancePassed)
                throw new UnauthorizedAccessException("Customer authentication result rejected.");
            await _customerRepository.AddAsync(customer, cancellationToken);

            Customer.Create(customer, request.UserId);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return _mapper.Map<CustomerResponseDto>(customer);
        }
    }
}
