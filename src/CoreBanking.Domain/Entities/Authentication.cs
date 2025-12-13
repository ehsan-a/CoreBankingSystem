using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Entities
{
    public class Authentication
    {
        public Guid Id { get; set; }
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
