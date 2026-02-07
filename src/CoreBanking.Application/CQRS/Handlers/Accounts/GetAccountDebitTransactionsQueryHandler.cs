using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountDebitTransactionsQueryHandler : IQueryHandler<GetAccountDebitTransactionsQuery, IEnumerable<TransactionResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountDebitTransactionsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionResponseDto>> Handle(GetAccountDebitTransactionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var account = await _accountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(account.DebitTransactions);
        }
    }
}
