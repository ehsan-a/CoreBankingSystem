using CoreBanking.Application.DTOs.Responses.ExternalServices;

namespace CoreBanking.Application.Interfaces
{
    public interface ICentralBankCreditCheckService
    {
        Task<CentralBankCreditCheckResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
