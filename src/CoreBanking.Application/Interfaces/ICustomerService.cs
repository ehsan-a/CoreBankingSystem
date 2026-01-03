using CoreBanking.Application.DTOs.Requests.Customer;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Domain.Entities;
using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateAsync(CreateCustomerRequestDto createCustomerRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<CustomerResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateCustomerRequestDto updateCustomerRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<Customer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
