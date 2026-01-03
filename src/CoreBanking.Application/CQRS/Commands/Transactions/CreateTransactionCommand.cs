using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Domain.Enums;

namespace CoreBanking.Application.CQRS.Commands.Transactions
{
    public class CreateTransactionCommand : ICommand<TransactionResponseDto>
    {
        public Guid DebitAccountId { get; set; }
        public Guid CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public Guid UserId { get; set; }
        public string IdempotencyKey { get; set; }
    }
}
