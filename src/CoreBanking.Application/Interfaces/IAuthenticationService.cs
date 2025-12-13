using CoreBanking.Application.DTOs;
using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IAuthenticationService
    {
        //Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        //Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        //Task CreateAsync(T entity, CancellationToken cancellationToken);
        //Task UpdateAsync(T entity, CancellationToken cancellationToken);
        //Task DeleteAsync(int id, CancellationToken cancellationToken);
        //Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
        Task<CivilRegistryResponseDto?> GetCivilRegistryAsync(string nationalCode);
        Task<PoliceClearanceResponseDto?> GetPoliceClearanceAsync(string nationalCode);
        Task<CentralBankCreditCheckResponseDto?> GetCentralBankCreditCheckAsync(string nationalCode);
    }
}
