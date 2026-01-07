using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.DTOs.Responses.Transaction;
using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponseDto> CreateAsync(CreateAccountRequestDto createAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<IEnumerable<AccountResponseDto>> GetAllAsync(int limit, int offset, CancellationToken cancellationToken);
        Task<AccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateAccountRequestDto updateAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TransactionResponseDto?>> GetDebitTransactionsAsync(Guid id, CancellationToken cancellationToken);
    }
}
