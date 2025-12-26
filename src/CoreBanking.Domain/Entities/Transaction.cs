using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Transactions;

namespace CoreBanking.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid DebitAccountId { get; set; }
        public Account? DebitAccount { get; set; } = default!;
        public Guid CreditAccountId { get; set; }
        public Account? CreditAccount { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public TransactionType Type { get; set; }

        public static Transaction Create(Transaction transaction, string idempotencyKey, Guid userId)
        {
            transaction.AddDomainEvent(
                new TransactionCreatedEvent(transaction, idempotencyKey, userId)
            );
            return transaction;
        }
    }
}
