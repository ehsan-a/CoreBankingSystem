using CoreBanking.Application.DTOs.Responses.ExternalServices;

namespace CoreBanking.Application.DTOs.Responses.Authentication
{
    public class AuthenticationResponseDto
    {
        public CivilRegistryResponseDto? civilRegistry { get; set; }
        public CentralBankCreditCheckResponseDto? centralBankCreditCheck { get; set; }
        public PoliceClearanceResponseDto? policeClearance { get; set; }
        public RegisteredAuthResponseDto? RegisteredAuthentication { get; set; }
    }
}
