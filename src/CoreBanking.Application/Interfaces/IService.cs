using System.Security.Claims;

namespace CoreBanking.Application.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(T entity, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
