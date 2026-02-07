using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Specifications.Customers;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerResponseDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponseDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var customers = await _customerRepository.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }
    }
}
