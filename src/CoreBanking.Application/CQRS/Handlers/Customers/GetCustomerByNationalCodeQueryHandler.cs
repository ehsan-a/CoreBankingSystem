using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetCustomerByNationalCodeQueryHandler : IQueryHandler<GetCustomerByNationalCodeQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByNationalCodeQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer?> Handle(GetCustomerByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();

            return await _customerRepository.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
        }
    }
}
