using AutoMapper;
using CoreBanking.Application.CQRS.Commands.Transactions;
using CoreBanking.Application.CQRS.Queries.Transactions;
using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CoreBanking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public TransactionService(IMapper mapper, UserManager<User> userManager, IMediator mediator)
        {
            _mapper = mapper;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<TransactionResponseDto> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, string idempotencyKey, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var command = _mapper.Map<CreateTransactionCommand>(createTransactionRequestDto);
            command.UserId = user.Id;
            command.IdempotencyKey = idempotencyKey;
            return await _mediator.Send(command);
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllTransactionsQuery());
        }

        public async Task<TransactionResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTransactionByIdQuery { Id = id });
        }
    }
}
