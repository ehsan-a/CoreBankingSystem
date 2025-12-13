using CoreBanking.Application.DTOs;

namespace CoreBanking.Application.Interfaces
{
    public interface ICentralBankCreditCheckService
    {
        Task<CentralBankCreditCheckResponseDto?> GetResultInfoAsync(string nationalCode);
    }
}
