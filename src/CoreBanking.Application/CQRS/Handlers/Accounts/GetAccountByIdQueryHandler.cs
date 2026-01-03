using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;

namespace CoreBanking.Application.CQRS.Handlers.Accounts
{
    public class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountResponseDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var account = await _unitOfWork.Accounts.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<AccountResponseDto>(account);
        }
    }
}
