using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountUpdatedEvent : IDomainEvent
    {
        public Account Account { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;
        public string? OldValue { get; }
        public Guid UserId { get; }

        public AccountUpdatedEvent(Account account, Guid userId, string? oldValue)
        {
            Account = account;
            UserId = userId;
            OldValue = oldValue;
        }
    }
}
