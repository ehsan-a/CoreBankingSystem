using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Transactions;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Transactions;

namespace CoreBanking.Application.CQRS.Handlers.Transactions
{
    public class GetAllTransactionsQueryHandler : IQueryHandler<GetAllTransactionsQuery, IEnumerable<TransactionResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionResponseDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var transactions = await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        }
    }
}
