using CoreBanking.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoreBanking.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string? AccountNumber { get; set; } = default!;

        public decimal Balance { get; set; } = default!;

        public AccountStatus Status { get; set; } = AccountStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; } = default!;

        public ICollection<Transaction>? DebitTransactions { get; set; } = new List<Transaction>();

        public ICollection<Transaction>? CreditTransactions { get; set; } = new List<Transaction>();
    }
}
