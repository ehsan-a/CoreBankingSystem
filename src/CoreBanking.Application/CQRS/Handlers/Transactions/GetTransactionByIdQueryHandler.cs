using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Transactions;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Transactions;

namespace CoreBanking.Application.CQRS.Handlers.Transactions
{
    public class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<TransactionResponseDto>(transaction);
        }
    }
}
