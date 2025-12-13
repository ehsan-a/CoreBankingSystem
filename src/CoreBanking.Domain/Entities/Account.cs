using CoreBanking.Domain.Enums;

namespace CoreBanking.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public string AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public ICollection<Transaction> DebitTransactions { get; set; } = new List<Transaction>();

        public ICollection<Transaction> CreditTransactions { get; set; } = new List<Transaction>();
    }
}
