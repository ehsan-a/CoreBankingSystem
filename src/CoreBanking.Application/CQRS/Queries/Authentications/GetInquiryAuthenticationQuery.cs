using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Authentication;

namespace CoreBanking.Application.CQRS.Queries.Authentications
{
    public class GetInquiryAuthenticationQuery : IQuery<AuthenticationResponseDto>
    {
        public string NationalCode { get; set; }
    }
}
