using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountCreatedEvent : IDomainEvent
    {
        public Account Account { get; }
        public Guid UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;

        public AccountCreatedEvent(Account account, Guid userId)
        {
            Account = account;
            UserId = userId;
        }
    }
}
