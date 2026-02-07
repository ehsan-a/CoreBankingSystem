using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Accounts
{
    public class AccountUpdatedEvent : INotification
    {
        public Account Account { get; }
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