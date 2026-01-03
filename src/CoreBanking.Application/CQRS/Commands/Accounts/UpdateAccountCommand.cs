using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Domain.Enums;

namespace CoreBanking.Application.CQRS.Commands.Accounts
{
    public class UpdateAccountCommand : ICommand
    {
        public Guid Id { get; set; }
        public AccountStatus Status { get; set; }
        public Guid UserId { get; set; }
    }
}
