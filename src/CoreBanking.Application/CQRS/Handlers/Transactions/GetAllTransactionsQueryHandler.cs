using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Transactions;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Specifications.Transactions;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Transactions
{
    public class GetAllTransactionsQueryHandler : IQueryHandler<GetAllTransactionsQuery, IEnumerable<TransactionResponseDto>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public GetAllTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionResponseDto>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            var transactions = await _transactionRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<TransactionResponseDto>>(transactions);
        }
    }
}
