using CoreBanking.Application.DTOs;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginRequestDto input);
        Task RegisterAsync(RegisterRequestDto input);
        Task LogoutAsync();
        Task<User> GetUserAsync(ClaimsPrincipal userPrincipal);

    }
}
