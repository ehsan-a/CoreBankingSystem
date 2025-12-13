using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.ExternalServices.CentralBankCreditCheck
{
    public class CentralBankCreditCheckDto
    {
        public int Id { get; set; }
        public string NationalCode { get; set; } = default!;
        public bool IsValid { get; set; }

        public string? Reason { get; set; }

        public DateTime CheckedAt { get; set; }
    }
}
