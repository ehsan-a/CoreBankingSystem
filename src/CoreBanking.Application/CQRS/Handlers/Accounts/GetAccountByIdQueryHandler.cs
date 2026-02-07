using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountResponseDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var account = await _accountRepository.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);
        }
    }
}
