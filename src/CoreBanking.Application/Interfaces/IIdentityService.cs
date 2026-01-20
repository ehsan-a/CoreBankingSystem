using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Identities;
using CoreBanking.Domain.Entities;
using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto input);
        Task RegisterAsync(RegisterRequestDto input);
        Task LogoutAsync(Guid userId);
        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

    }
}
