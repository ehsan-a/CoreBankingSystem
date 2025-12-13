using CoreBanking.Application.DTOs;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode);
        Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode);
        Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode);
        Task<Authentication> CreateAsync(AuthenticationResponseDto authenticationResponse, CancellationToken cancellationToken);
        Task<Authentication?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
