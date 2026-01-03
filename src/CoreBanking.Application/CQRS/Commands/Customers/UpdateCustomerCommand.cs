using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Commands.Customers
{
    public class UpdateCustomerCommand : ICommand
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}
