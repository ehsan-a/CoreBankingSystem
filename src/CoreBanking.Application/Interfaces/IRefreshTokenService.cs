using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateTokenAsync(Guid userId);
        Task<RefreshToken?> GetTokenAsync(string refreshToken);
        Task RevokeTokenAsync(Guid userId, string reason);
        Task SaveChangesAsync();
    }
}
