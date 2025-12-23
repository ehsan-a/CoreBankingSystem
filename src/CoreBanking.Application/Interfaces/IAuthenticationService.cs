using CoreBanking.Application.DTOs.Requests.Authentication;
using CoreBanking.Application.DTOs.Responses.Authentication;
using CoreBanking.Application.DTOs.Responses.ExternalServices;
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
        Task<Authentication> CreateAsync(CreateAuthenticationRequestDto createAuthenticationRequestDto, CancellationToken cancellationToken);
        Task<Authentication?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(string nationalCode, CancellationToken cancellationToken);
        Task<AuthenticationResponseDto> GetInquiryAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
