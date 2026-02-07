using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly INumberGenerator _numberGenerator;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, ICustomerRepository customerRepository, IMapper mapper, INumberGenerator numberGenerator)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _numberGenerator = numberGenerator;
        }

        public async Task<AccountResponseDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customerExists = await _customerRepository.ExistsByIdAsync(request.CustomerId, cancellationToken);

            if (!customerExists)
            {
                throw new NotFoundException("Customer", request.CustomerId);
            }
            var accountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            var account = Account.Create(accountNumber, request.CustomerId, request.UserId);
            await _accountRepository.AddAsync(account, cancellationToken);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);

        }
    }
}
