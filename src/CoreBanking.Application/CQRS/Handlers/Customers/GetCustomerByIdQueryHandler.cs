using AutoMapper;
using CoreBanking.Application.CQRS.Interfaces;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Customers;

namespace CoreBanking.Application.CQRS.Handlers.Customers
{
    public class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CustomerGetAllSpec();
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id, spec, cancellationToken);
            return _mapper.Map<CustomerResponseDto>(customer);
        }
    }
}
