using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs.Requests.Authentication
{
    public class CreateAuthenticationRequestDto
    {
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
    }
}
