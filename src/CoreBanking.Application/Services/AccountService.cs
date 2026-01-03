using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Accounts;
using CoreBanking.Application.CQRS.Queries.Accounts;
using CoreBanking.Application.DTOs.Requests.Account;
using CoreBanking.Application.DTOs.Responses.Account;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoreBanking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public AccountService(IMapper mapper, UserManager<User> userManager, IMediator mediator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<AccountResponseDto> CreateAsync(CreateAccountRequestDto createAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<CreateAccountCommand>(createAccountRequestDto);
            command.UserId = user.Id;
            return await _mediator.Send(command);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = new DeleteAccountCommand { Id = id, UserId = user.Id };
            await _mediator.Send(command);
        }

        public async Task<IEnumerable<AccountResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllAccountsQuery());
        }

        public async Task<AccountResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAccountByIdQuery { Id = id });

        }

        public async Task UpdateAsync(UpdateAccountRequestDto updateAccountRequestDto, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<UpdateAccountCommand>(updateAccountRequestDto);
            command.UserId = user.Id;
            await _mediator.Send(command);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAccountExistsQuery { Id = id });
        }

    }
}
