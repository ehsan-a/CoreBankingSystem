using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Queries.Authentications
{
    public class GetAuthenticationExistsQuery : IQuery<bool>
    {
        public string NationalCode { get; set; }
    }
}
