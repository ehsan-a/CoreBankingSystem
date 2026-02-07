using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountDeletedEvent : INotification
    {
        public Account Account { get; }
        public Guid UserId { get; }

        public AccountDeletedEvent(Account account, Guid userId)
        {
            Account = account;
            UserId = userId;
        }
    }
}