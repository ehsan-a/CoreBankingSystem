using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Queries.Customers
{
    public class GetCustomerByNationalCodeQuery : IQuery<Customer>
    {
        public string NationalCode { get; set; }
    }
}
