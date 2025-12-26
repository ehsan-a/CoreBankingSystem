using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerDeletedEvent : IDomainEvent
    {
        public Customer Customer { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;
        public Guid UserId { get; }

        public CustomerDeletedEvent(Customer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}
