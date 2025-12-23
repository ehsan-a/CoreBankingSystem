using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CoreBanking.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task LoginAsync(LoginRequestDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var validPassword = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!validPassword)
                throw new UnauthorizedAccessException("Invalid email or password");

            await _signInManager.SignInAsync(user, isPersistent: input.RememberMe);
            _logger.LogInformation("User logged in successfully.");
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

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully.");
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
                throw new ArgumentNullException(nameof(userPrincipal));

            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            return user;
        }
    }
}
