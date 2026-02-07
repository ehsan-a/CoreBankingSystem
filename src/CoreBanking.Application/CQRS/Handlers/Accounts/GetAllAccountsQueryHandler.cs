using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountResponseDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResponseDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec(request.Limit, request.Offset);
            var accounts = await _accountRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<AccountResponseDto>>(accounts);
        }
    }
}
