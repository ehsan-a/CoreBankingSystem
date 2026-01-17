using Ardalis.GuardClauses;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Exceptions;

namespace CoreBanking.Domain.Extensions
{
    public static class AccountGuards
    {
        public static void Debit(this IGuardClause guardClause, Account account, decimal amount)
        {
            if (account.Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");

            if (account.Balance < amount)
                throw new DomainException("Insufficient funds");
        }

        public static void Credit(this IGuardClause guardClause, Account account, decimal amount)
        {
            if (account.Status != AccountStatus.Active)
                throw new DomainException("Account is not active");

            if (amount <= 0)
                throw new DomainException("Invalid amount");
        }
    }
}
