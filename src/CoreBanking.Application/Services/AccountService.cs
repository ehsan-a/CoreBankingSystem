using AutoMapper;
using CoreBanking.Application.Exceptions;
using CoreBanking.Application.Interfaces;
using CoreBanking.Application.Specifications.Accounts;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace CoreBanking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INumberGenerator _numberGenerator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuditLogService _auditLogService;

        public AccountService(IUnitOfWork unitOfWork, INumberGenerator numberGenerator, IMapper mapper, UserManager<User> userManager, IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _numberGenerator = numberGenerator;
            _mapper = mapper;
            _userManager = userManager;
            _auditLogService = auditLogService;
        }

        public async Task CreateAsync(Account account, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var customerExists = await _unitOfWork.Customers.ExistsByIdAsync(account.CustomerId, cancellationToken);

            if (!customerExists)
            {
                throw new NotFoundException("Customer", account.CustomerId);
            }
            account.AccountNumber = await _numberGenerator.GenerateAccountNumberAsync();
            await _unitOfWork.Accounts.AddAsync(account, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await CreateAuditLogAsync(account, principal, cancellationToken, AuditActionType.Create);
        }
        public async Task DeleteAsync(Guid id, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            var item = await _unitOfWork.Accounts.GetByIdAsync(id, spec, cancellationToken);
            if (item == null) return;
            _unitOfWork.Accounts.Delete(item);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await CreateAuditLogAsync(item, principal, cancellationToken, AuditActionType.Delete);
        }

        public async Task<IEnumerable<Account>> GetAllAsync(CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.GetAllAsync(spec, cancellationToken);
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.GetByIdAsync(id, spec, cancellationToken);
        }

        public async Task UpdateAsync(Account account, ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            var oldAccount = await _unitOfWork.Accounts
                .GetByIdAsNoTrackingAsync(account.Id, cancellationToken);

            var oldValue = JsonSerializer.Serialize(oldAccount);

            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await CreateAuditLogAsync(account, principal, cancellationToken, AuditActionType.Update, oldValue);
        }
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            var spec = new AccountGetAllSpec();
            return await _unitOfWork.Accounts.ExistsByIdAsync(id, spec, cancellationToken);
        }

        public async Task CreateAuditLogAsync(Account account, ClaimsPrincipal principal, CancellationToken cancellationToken, AuditActionType auditActionType, string? oldValue = null)
        {
            var user = await _userManager.GetUserAsync(principal);

            var auditLog = _mapper.Map<AuditLog>(account);
            auditLog.ActionType = auditActionType;
            auditLog.PerformedBy = user.Id.ToString();
            auditLog.OldValue = oldValue;
            await _auditLogService.LogAsync(auditLog, cancellationToken);
        }
    }
}
