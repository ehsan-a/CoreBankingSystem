using CoreBanking.Application.DTOs;

namespace CoreBanking.Application.Interfaces
{
    public interface IPoliceClearanceService
    {
        Task<PoliceClearanceResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
