using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, IEnumerable<AccountResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountResponseDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var accounts = await _unitOfWork.Accounts.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<AccountResponseDto>>(accounts);
        }
    }
}
