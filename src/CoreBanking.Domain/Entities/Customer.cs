namespace CoreBanking.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Account>? Accounts { get; set; } = new List<Account>();
        public User? User { get; set; }
    }
}
