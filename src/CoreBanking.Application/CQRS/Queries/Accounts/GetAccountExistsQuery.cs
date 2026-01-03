using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Queries.Accounts
{
    public class GetAccountExistsQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}

