using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var item = await _accountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            if (item == null) return;

            Account.Delete(item, request.UserId);

            _accountRepository.Delete(item);
            await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
