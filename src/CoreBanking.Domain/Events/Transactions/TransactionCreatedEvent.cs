using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Transactions
{
    public class TransactionCreatedEvent : INotification
    {
        public Transaction Transaction { get; }
        public string IdempotencyKey { get; }
        public Guid UserId { get; }

        public TransactionCreatedEvent(Transaction transaction, string idempotencyKey, Guid userId)
        {
            Transaction = transaction;
            IdempotencyKey = idempotencyKey;
            UserId = userId;
        }
    }

}