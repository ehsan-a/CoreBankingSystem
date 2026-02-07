using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerUpdatedEvent : INotification
    {
        public Customer Customer { get; }
        public string? OldValue { get; }
        public Guid UserId { get; }

        public CustomerUpdatedEvent(Customer customer, Guid userId, string? oldValue)
        {
            Customer = customer;
            UserId = userId;
            OldValue = oldValue;
        }
    }
}