using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Events.Customers
{
    public class CustomerUpdatedEvent : IDomainEvent
    {
        public Customer Customer { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;
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
