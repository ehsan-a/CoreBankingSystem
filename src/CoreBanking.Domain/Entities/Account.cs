using Ardalis.GuardClauses;
using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Accounts;
using CoreBanking.Domain.Extensions;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Account : BaseEntity, ISoftDeletable, IAggregateRoot
    {
        public Account(string accountNumber, Guid customerId)
        {
            Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            Guard.Against.NullOrEmpty(accountNumber, nameof(accountNumber));

            AccountNumber = accountNumber;
            CustomerId = customerId;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private Account() { }

        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; } = 0;
        public AccountStatus Status { get; private set; } = AccountStatus.Active;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }
        public bool IsDeleted { get; set; } = false;

        private readonly List<Transaction> _debitTransactions = new List<Transaction>();
        private readonly List<Transaction> _creditTransactions = new List<Transaction>();

        public IReadOnlyCollection<Transaction>? DebitTransactions => _debitTransactions.AsReadOnly();
        public IReadOnlyCollection<Transaction>? CreditTransactions => _creditTransactions.AsReadOnly();

        public void Debit(decimal amount)
        {
            Guard.Against.Debit(this, amount);

            Balance -= amount;
        }

        public void Credit(decimal amount)
        {
            Guard.Against.Credit(this, amount);

            Balance += amount;
        }

        public void ChangeStatus(AccountStatus status)
        {
            Guard.Against.EnumOutOfRange(status, nameof(status));
            Status = status;
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
