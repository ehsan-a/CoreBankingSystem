using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountDeletedEvent : IDomainEvent
    {
        public Account Account { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;
        public Guid UserId { get; }

        public AccountDeletedEvent(Account account, Guid userId)
        {
            Account = account;
            UserId = userId;
        }
    }
}
