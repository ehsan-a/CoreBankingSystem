using AutoMapper;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Accounts;

namespace CoreBanking.Application.EventHandlers.Accounts
{
    public class AccountDeletedAuditHandler : IDomainEventHandler<AccountDeletedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public AccountDeletedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(AccountDeletedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
            auditLog.ActionType = AuditActionType.Delete;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
