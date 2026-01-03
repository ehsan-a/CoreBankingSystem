using CoreBanking.Application.CQRS.Interfaces;

namespace CoreBanking.Application.CQRS.Queries.Customers
{
    public class GetCustomerExistsQuery : IQuery<bool>
    {
        public Guid Id { get; set; }
    }
}
