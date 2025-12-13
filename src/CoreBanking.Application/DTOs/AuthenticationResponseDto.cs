using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.DTOs
{
    public class AuthenticationResponseDto
    {
        public CivilRegistryResponseDto? civilRegistry { get; set; }
        public CentralBankCreditCheckResponseDto? centralBankCreditCheck { get; set; }
        public PoliceClearanceResponseDto? policeClearance { get; set; }
        public Authentication? RegisteredAuthentication { get; set; }
    }
}
