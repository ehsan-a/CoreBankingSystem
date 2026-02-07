using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetCustomerExistsQueryHandler : IQueryHandler<GetCustomerExistsQuery, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerExistsQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(GetCustomerExistsQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.ExistsByIdAsync(request.Id, cancellationToken);
        }
    }
}