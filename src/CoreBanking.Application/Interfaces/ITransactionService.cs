using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponseDto> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, string idempotencyKey, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<TransactionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
