using CoreBanking.Application.DTOs.Responses.ExternalServices;

namespace CoreBanking.Application.DTOs.Responses.Authentication
{
    public class AuthenticationResponseDto
    {
        public CivilRegistryResponseDto? CivilRegistry { get; set; }
        public CentralBankCreditCheckResponseDto? CentralBankCreditCheck { get; set; }
        public PoliceClearanceResponseDto? PoliceClearance { get; set; }
        public RegisteredAuthResponseDto? RegisteredAuthentication { get; set; }
    }
}
