using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IIdempotencyService
    {
        Task<string?> GetResultAsync(string key, Guid userId);
        Task SaveResultAsync(string key, object result, Guid userId);
    }
}
