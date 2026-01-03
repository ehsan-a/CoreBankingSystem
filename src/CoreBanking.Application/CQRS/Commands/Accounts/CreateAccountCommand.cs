using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Account;

namespace CoreBanking.Application.CQRS.Commands.Accounts
{
    public class CreateAccountCommand : ICommand<AccountResponseDto>
    {
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }
    }
}
