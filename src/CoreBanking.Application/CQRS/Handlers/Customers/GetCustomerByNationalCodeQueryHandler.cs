using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Entities;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetCustomerByNationalCodeQueryHandler : IQueryHandler<GetCustomerByNationalCodeQuery, Customer>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByNationalCodeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> Handle(GetCustomerByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();

            return await _unitOfWork.Customers.GetByNationalCodeAsync(request.NationalCode, spec, cancellationToken);
        }
    }
}
