using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountCreatedEvent : INotification
    {
        public Account Account { get; }
        public Guid UserId { get; }

        public AccountCreatedEvent(Account account, Guid userId)
        {
            Account = account;
            UserId = userId;
        }
    }
}
