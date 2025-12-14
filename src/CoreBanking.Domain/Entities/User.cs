using Microsoft.AspNetCore.Identity;


namespace CoreBanking.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; } = default!;
    }
}
