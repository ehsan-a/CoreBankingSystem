using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Commands.Accounts
{
    public class DeleteAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
