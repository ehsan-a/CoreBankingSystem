using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, string idempotencyKey, CancellationToken cancellationToken);
        Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken);
        Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
