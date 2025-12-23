using CoreBanking.Application.DTOs.Responses.ExternalServices;

namespace CoreBanking.Application.Interfaces
{
    public interface ICivilRegistryService
    {
        Task<CivilRegistryResponseDto?> GetPersonInfoAsync(string nationalCode);
    }
}
