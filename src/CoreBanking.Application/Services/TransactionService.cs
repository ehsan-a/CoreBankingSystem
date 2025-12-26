using AutoMapper;
using CoreBanking.Application.DTOs.Requests.Transaction;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Application.Specifications.Transactions;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.Json;

namespace CoreBanking.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IIdempotencyService _idempotencyService;
        public TransactionService(IUnitOfWork unitOfWork, IAuditLogService auditLogService, IMapper mapper, UserManager<User> userManager, IIdempotencyService idempotencyService)
        {
            _unitOfWork = unitOfWork;
            _auditLogService = auditLogService;
            _mapper = mapper;
            _userManager = userManager;
            _idempotencyService = idempotencyService;
        }
        public async Task<Transaction> CreateAsync(CreateTransactionRequestDto createTransactionRequestDto, ClaimsPrincipal principal, string idempotencyKey, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(principal);
            var previousResult = await _idempotencyService.GetResultAsync(idempotencyKey, user.Id);
            if (previousResult != null)
            {
                throw new ConflictException(previousResult);
            }

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var spec = new AccountGetAllSpec();
                var from = await _unitOfWork.Accounts.GetByIdAsync(createTransactionRequestDto.DebitAccountId, spec, cancellationToken);
                var to = await _unitOfWork.Accounts.GetByIdAsync(createTransactionRequestDto.CreditAccountId, spec, cancellationToken);

                from.Debit(createTransactionRequestDto.Amount);
                to.Credit(createTransactionRequestDto.Amount);

                var transaction = _mapper.Map<Transaction>(createTransactionRequestDto);
                await _unitOfWork.Transactions.AddAsync(transaction, cancellationToken);
                Transaction.Create(transaction, idempotencyKey, user.Id);

                await _unitOfWork.CommitAsync();
                return transaction;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            return await _unitOfWork.Transactions.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new TransactionGetAllSpec();
            return await _unitOfWork.Transactions.GetByIdAsync(id, spec, cancellationToken);
        }
    }
}
