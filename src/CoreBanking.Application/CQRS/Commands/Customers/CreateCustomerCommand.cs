using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Customer;

namespace CoreBanking.Application.CQRS.Commands.Customers
{
    public class CreateCustomerCommand : ICommand<CustomerResponseDto>
    {
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}
