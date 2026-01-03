using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Transaction;

namespace CoreBanking.Application.CQRS.Queries.Transactions
{
    public class GetTransactionByIdQuery : IQuery<TransactionResponseDto>
    {
        public Guid Id { get; set; }
    }
}
