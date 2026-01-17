using Ardalis.GuardClauses;
using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Events.Authentications;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Entities
{
    public class Authentication : BaseEntity, IAggregateRoot
    {
        public Authentication(string nationalCode, bool centralBankCreditCheckPassed, bool civilRegistryVerified, bool policeClearancePassed)
        {
            Guard.Against.NullOrEmpty(nationalCode, nameof(nationalCode));

            NationalCode = nationalCode;
            CentralBankCreditCheckPassed = centralBankCreditCheckPassed;
            CivilRegistryVerified = civilRegistryVerified;
            PoliceClearancePassed = policeClearancePassed;
        }

#pragma warning disable CS8618 // Required by Entity Framework
        private Authentication() { }

        public string NationalCode { get; private set; }
        public bool CentralBankCreditCheckPassed { get; private set; }
        public bool CivilRegistryVerified { get; private set; }
        public bool PoliceClearancePassed { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public static Authentication Create(Authentication authentication, Guid userId)
        {
            authentication.AddDomainEvent(
                new AuthenticationCreatedEvent(authentication, userId)
            );
            return authentication;
        }
    }
}
