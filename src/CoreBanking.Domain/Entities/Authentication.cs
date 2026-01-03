using CoreBanking.Domain.Abstracttion;
using CoreBanking.Domain.Events.Authentications;
using CoreBanking.Domain.Events.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Entities
{
    public class Authentication : BaseEntity
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public static Authentication Create(Authentication authentication, Guid userId)
        {
            authentication.AddDomainEvent(
                new AuthenticationCreatedEvent(authentication, userId)
            );
            return authentication;
        }
    }
}
