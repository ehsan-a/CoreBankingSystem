using AutoMapper;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Accounts;

namespace CoreBanking.Application.EventHandlers.Accounts
{
    public class AccountUpdatedAuditHandler : IDomainEventHandler<AccountUpdatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public AccountUpdatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(AccountUpdatedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
            auditLog.ActionType = AuditActionType.Update;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            auditLog.OldValue = domainEvent.OldValue;
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
