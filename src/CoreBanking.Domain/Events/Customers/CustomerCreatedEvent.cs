using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerCreatedEvent : INotification
    {
        public Customer Customer { get; }
        public Guid UserId { get; }

        public CustomerCreatedEvent(Customer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}