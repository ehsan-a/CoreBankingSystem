using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CoreBanking.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IdentityService> _logger;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityService(UserManager<User> userManager, ILogger<IdentityService> logger, IJwtTokenService jwtTokenService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<(string AccessToken, string RefreshToken)> LoginAsync(LoginRequestDto input)
        {

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var validPassword = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid email or password");

            _logger.LogInformation("User logged in successfully.");

            var accessToken = await _jwtTokenService.GenerateTokenAsync(user);
            var refreshToken = await _refreshTokenService.GenerateTokenAsync(user.Id);
            return (accessToken, refreshToken);
        }

        public async Task RegisterAsync(RegisterRequestDto input)
        {
            var user = new User
            {
                UserName = input.Email,
                Email = input.Email,
                CustomerId = input.CustomerId
            };

            var result = await _userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            _logger.LogInformation("User registered successfully.");
        }

        public async Task LogoutAsync(Guid userId)
        {
            await _refreshTokenService.RevokeTokenAsync(userId, "Logout");
            await _refreshTokenService.SaveChangesAsync();
            _logger.LogInformation("User logged out successfully.");
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
                throw new ArgumentNullException(nameof(userPrincipal));

            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
                throw new BadHttpRequestException("Invalid Request");

            return user;
        }

        public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenService.GetTokenAsync(refreshToken);

            if (token == null || token.IsExpired || token.IsRevoked)
                throw new UnauthorizedAccessException("Invalid refresh token");

            token.RevokedAt = DateTime.Now;
            token.RevokedReason = "Rotated";
            await _refreshTokenService.SaveChangesAsync();

            var newRefreshToken = await _refreshTokenService.GenerateTokenAsync(token.UserId);
            var newAccessToken = await _jwtTokenService.GenerateTokenAsync(token.User);

            return (newAccessToken, newRefreshToken);
        }
    }
}
