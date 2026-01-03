using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponseDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var customers = await _unitOfWork.Customers.GetAllAsync(spec, cancellationToken);
            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }
    }
}
