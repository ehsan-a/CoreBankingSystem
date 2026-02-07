using Ardalis.GuardClauses;
using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Events.Customers;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Customer : BaseEntity, ISoftDeletable, IAggregateRoot
    {
        private Customer(string nationalCode, string firstName, string lastName)
        {
            Guard.Against.NullOrEmpty(nationalCode, nameof(nationalCode));
            Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            Guard.Against.NullOrEmpty(lastName, nameof(lastName));

            NationalCode = nationalCode;
            FirstName = firstName;
            LastName = lastName;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private Customer() { }

        public string NationalCode { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public User? User { get; private set; }
        public bool IsDeleted { get; set; } = false;

        private readonly List<Account> _accounts = new List<Account>();
        public IReadOnlyCollection<Account>? Accounts => _accounts.AsReadOnly();

        public static Customer Create(string nationalCode, string firstName, string lastName, Guid userId)
        {
            var customer = new Customer(nationalCode, firstName, lastName);
            customer.AddDomainEvent(new CustomerCreatedEvent(customer, userId));
            return customer;
        }

        public static Customer Delete(Customer customer, Guid userId)
        {
            customer.AddDomainEvent(
                new CustomerDeletedEvent(customer, userId)
            );
            return customer;
        }

        public static Customer Update(Customer customer, Guid userId, string oldValue)
        {
            customer.AddDomainEvent(
                new CustomerUpdatedEvent(customer, userId, oldValue)
            );
            return customer;
        }
    }
}
