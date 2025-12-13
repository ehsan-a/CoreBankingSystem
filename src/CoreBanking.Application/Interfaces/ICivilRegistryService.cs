using CoreBanking.Application.DTOs;

namespace CoreBanking.Application.Interfaces
{
    public interface ICivilRegistryService
    {
        Task<CivilRegistryResponseDto?> GetPersonInfoAsync(string nationalCode);
    }
}
