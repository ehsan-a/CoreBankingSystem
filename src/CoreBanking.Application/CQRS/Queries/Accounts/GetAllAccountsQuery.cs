using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.DTOs.Responses.Account;

namespace CoreBanking.Application.CQRS.Queries.Accounts
{
    public class GetAllAccountsQuery : IQuery<IEnumerable<AccountResponseDto>>
    {
    }
}
