using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Events.Customers;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Customer : BaseEntity, ISoftDeletable
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Account>? Accounts { get; set; } = new List<Account>();
        public User? User { get; set; }
        public bool IsDeleted { get; set; } = false;

        public static Customer Create(Customer customer, Guid userId)
        {
            customer.AddDomainEvent(
                new CustomerCreatedEvent(customer, userId)
            );
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
