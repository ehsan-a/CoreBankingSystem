using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Events.Transactions
{
    public class TransactionCreatedEvent : IDomainEvent
    {
        public Transaction Transaction { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;
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
