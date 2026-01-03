using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<RegisteredAuthResponseDto> CreateAsync(CreateAuthenticationRequestDto createAuthenticationRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<AuthenticationResponseDto?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken);
        Task<AuthenticationResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
