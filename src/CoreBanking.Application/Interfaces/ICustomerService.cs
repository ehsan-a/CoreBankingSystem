using CoreBanking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface ICustomerService : IService<Customer>
    {
        Task<Customer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
    }
}
