using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Commands.Customers
{
    public class DeleteCustomerCommand:ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
