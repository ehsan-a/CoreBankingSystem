using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Entities
{
    public class Authentication
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public DateTime Date { get; set; }
        public bool CentralBankCreditCheckStatus { get; set; }
        public bool CivilRegistryStatus { get; set; }
        public bool PoliceClearanceStatus { get; set; }
    }
}
