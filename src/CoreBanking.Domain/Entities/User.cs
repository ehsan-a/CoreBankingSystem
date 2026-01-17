using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;


namespace CoreBanking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User(Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));

            CustomerId = customerId;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private User() { }

        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
    }
}
