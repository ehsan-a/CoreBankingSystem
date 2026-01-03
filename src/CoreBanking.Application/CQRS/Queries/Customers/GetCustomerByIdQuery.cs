using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Customer;

namespace CoreBanking.Application.CQRS.Queries.Customers
{
    public class GetCustomerByIdQuery : IQuery<CustomerResponseDto>
    {
        public Guid Id { get; set; }
    }
}
