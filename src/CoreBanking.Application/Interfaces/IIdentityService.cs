using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(string AccessToken, string RefreshToken)> LoginAsync(LoginRequestDto input);
        Task RegisterAsync(RegisterRequestDto input);
        Task LogoutAsync(Guid userId);
        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal);
        Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);

    }
}
