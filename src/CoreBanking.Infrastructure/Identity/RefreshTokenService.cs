using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Infrastructure.Generators;
using CoreBanking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Infrastructure.Identity
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly CoreBankingContext _context;

        public RefreshTokenService(CoreBankingContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateTokenAsync(Guid userId)
        {

            await RevokeTokenAsync(userId, "New Login");

            var refreshToken = new RefreshToken
            {
                Token = SecureTokenGenerator.GenerateSecureToken(),
                UserId = userId,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(30)
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken.Token;
        }
        public async Task<RefreshToken?> GetTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens
          .Include(x => x.User)
          .FirstOrDefaultAsync(x => x.Token == refreshToken);
        }
        public async Task<IEnumerable<RefreshToken>> GetActiveTokenAsync(Guid userId)
        {
            return await _context.RefreshTokens
           .Where(x => x.UserId == userId && x.RevokedAt == null)
           .ToListAsync();
        }
        public async Task RevokeTokenAsync(Guid userId, string reason)
        {
            var tokens = await GetActiveTokenAsync(userId);

            foreach (var token in tokens)
            {
                token.RevokedAt = DateTime.Now;
                token.RevokedReason = reason;
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
