using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Responses.Authentication
{
    public class RegisteredAuthResponseDto
    {
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
