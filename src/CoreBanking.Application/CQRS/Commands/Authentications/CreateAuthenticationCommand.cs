using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Authentication;

namespace CoreBanking.Application.CQRS.Commands.Authentications
{
    public class CreateAuthenticationCommand : ICommand<RegisteredAuthResponseDto>
    {
        public string NationalCode { get; set; }
        public bool CentralBankCreditCheckPassed { get; set; }
        public bool CivilRegistryVerified { get; set; }
        public bool PoliceClearancePassed { get; set; }
        public Guid UserId { get; set; }
    }
}
