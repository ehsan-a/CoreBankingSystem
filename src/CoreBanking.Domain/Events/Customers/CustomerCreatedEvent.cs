using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerCreatedEvent : IDomainEvent
    {
        public Customer Customer { get; }
        public Guid UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;

        public CustomerCreatedEvent(Customer customer, Guid userId)
        {
            Customer = customer;
            UserId = userId;
        }
    }
}
