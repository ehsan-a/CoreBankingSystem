using CoreBanking.Application.Interfaces;
using CoreBanking.Infrastructure.Persistence;
using CoreBanking.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoreBanking.Infrastructure.Idempotency
{
    public class IdempotencyService : IIdempotencyService
    {
        private readonly CoreBankingContext _context;

        public IdempotencyService(CoreBankingContext context)
        {
            _context = context;
        }

        public async Task<string?> GetResultAsync(string key, Guid userId)
        {
            var record = await _context.IdempotencyKeys
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId);
            return record?.Result;
        }

        public async Task SaveResultAsync(string key, object result, Guid userId)
        {
            var record = new IdempotencyKey
            {
                Key = key,
                Result = JsonSerializer.Serialize(result),
                CreatedAt = DateTime.Now,
                UserId = userId
            };
            _context.IdempotencyKeys.Add(record);
            await _context.SaveChangesAsync();
        }
    }

}
