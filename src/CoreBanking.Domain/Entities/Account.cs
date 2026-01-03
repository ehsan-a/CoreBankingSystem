using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Accounts;
using CoreBanking.Domain.Exceptions;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Account : BaseEntity, ISoftDeletable
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

        public static Account Create(Account account, Guid userId)
        {
            account.AddDomainEvent(
                new AccountCreatedEvent(account, userId)
            );
            return account;
        }

        public static Account Delete(Account account, Guid userId)
        {
            account.AddDomainEvent(
                new AccountDeletedEvent(account, userId)
            );
            return account;
        }

        public static Account Update(Account account, Guid userId, string oldValue)
        {
            account.AddDomainEvent(
                new AccountUpdatedEvent(account, userId, oldValue)
            );
            return account;
        }
    }
}
