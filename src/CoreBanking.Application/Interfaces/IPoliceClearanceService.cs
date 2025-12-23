using CoreBanking.Application.DTOs.Responses.ExternalServices;

namespace CoreBanking.Application.Interfaces
{
    public interface IPoliceClearanceService
    {
        Task<PoliceClearanceResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
