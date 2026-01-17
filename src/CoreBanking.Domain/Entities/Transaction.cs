using Ardalis.GuardClauses;
using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Transactions;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Transaction : BaseEntity, IAggregateRoot
    {
        public Transaction(
            Guid debitAccountId,
            Guid creditAccountId,
            decimal amount,
            string description,
            TransactionType type)
        {
            Guard.Against.NullOrEmpty(debitAccountId, nameof(debitAccountId));
            Guard.Against.NullOrEmpty(creditAccountId, nameof(creditAccountId));
            Guard.Against.NegativeOrZero(amount, nameof(amount));
            Guard.Against.NullOrEmpty(description, nameof(description));
            Guard.Against.EnumOutOfRange(type, nameof(type));

            DebitAccountId = debitAccountId;
            CreditAccountId = creditAccountId;
            Amount = amount;
            Description = description;
            Type = type;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private Transaction() { }

        public Guid DebitAccountId { get; private set; }
        public Guid CreditAccountId { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public TransactionType Type { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Account? DebitAccount { get; private set; }
        public Account? CreditAccount { get; private set; }

        public static Transaction Create(Transaction transaction, string idempotencyKey, Guid userId)
        {
            transaction.AddDomainEvent(
                new TransactionCreatedEvent(transaction, idempotencyKey, userId)
            );
            return transaction;
        }
    }
}
