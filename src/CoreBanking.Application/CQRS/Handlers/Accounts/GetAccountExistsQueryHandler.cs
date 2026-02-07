using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountExistsQueryHandler : IQueryHandler<GetAccountExistsQuery, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountExistsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(GetAccountExistsQuery request, CancellationToken cancellationToken)
        {
            return await _accountRepository.ExistsByIdAsync(request.Id, cancellationToken);
        }
    }
}
