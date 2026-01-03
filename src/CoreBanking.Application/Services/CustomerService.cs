using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Customers;
using CoreBanking.Application.CQRS.Queries.Customers;
using CoreBanking.Application.DTOs.Requests.Customer;
using CoreBanking.Application.DTOs.Responses.Customer;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoreBanking.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerService(UserManager<User> userManager, IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CustomerResponseDto> CreateAsync(CreateCustomerRequestDto createCustomerRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<CreateCustomerCommand>(createCustomerRequestDto);
            command.UserId = user.Id;
            return await _mediator.Send(command);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = new DeleteCustomerCommand { Id = id, UserId = user.Id };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllCustomersQuery());
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCustomerByIdQuery { Id = id });
        }

        public async Task<Customer?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCustomerByNationalCodeQuery { NationalCode = nationalCode });
        }

        public async Task UpdateAsync(UpdateCustomerRequestDto updateCustomerRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<UpdateCustomerCommand>(updateCustomerRequestDto);
            command.UserId = user.Id;
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetCustomerExistsQuery { Id = id });
        }
    }
}
