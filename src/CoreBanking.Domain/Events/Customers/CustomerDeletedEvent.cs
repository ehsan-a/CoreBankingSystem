using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerDeletedEvent : INotification
    {
        public Customer Customer { get; }
        public Guid UserId { get; }

        public CustomerDeletedEvent(Customer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}
