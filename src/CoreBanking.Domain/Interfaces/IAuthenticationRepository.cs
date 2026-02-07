using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Interfaces
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        Task<Authentication?> GetByNationalCodeAsync(string nationalCode, ISpecification<Authentication> spec, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
        Task<Authentication> AddAsync(Authentication authentication, CancellationToken cancellationToken = default);

    }
}