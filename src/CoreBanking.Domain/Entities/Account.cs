using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Exceptions;
using CoreBanking.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CoreBanking.Domain.Entities
{
    public class Account : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string? AccountNumber { get; set; } = default!;

        public decimal Balance { get; private set; } = default!;

        public AccountStatus Status { get; private set; } = AccountStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;

        public ICollection<Transaction>? DebitTransactions { get; set; } = new List<Transaction>();

        public ICollection<Transaction>? CreditTransactions { get; set; } = new List<Transaction>();

        public void Debit(decimal amount)
        {
            if (Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");

            if (Balance < amount)
                throw new DomainException("Insufficient funds");

            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            if (Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");

            Balance += amount;
        }
    }
}
