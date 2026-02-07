using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System.Text.Json;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);
            if (account == null) throw new NotFoundException("");

            _mapper.Map(request, account);

            var oldAccount = await _accountRepository
              .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _accountRepository.Update(account);

            Account.Update(account, request.UserId, oldValue);

            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
