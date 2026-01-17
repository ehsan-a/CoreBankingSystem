using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, AccountResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INumberGenerator _numberGenerator;

        public CreateAccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INumberGenerator numberGenerator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _numberGenerator = numberGenerator;
        }

        public async Task<AccountResponseDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var customerExists = await _unitOfWork.Customers.ExistsByIdAsync(request.CustomerId, cancellationToken);

            if (!customerExists)
            {
                throw new NotFoundException("Customer", request.CustomerId);
            }
            var accountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            var account = new Account(accountNumber, request.CustomerId);
            await _unitOfWork.Accounts.AddAsync(account, cancellationToken);

            Account.Create(account, request.UserId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);

        }
    }
}
