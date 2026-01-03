using AutoMapper;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Accounts;

namespace CoreBanking.Application.EventHandlers.Accounts
{
    public class AccountCreatedAuditHandler : IDomainEventHandler<AccountCreatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public AccountCreatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(AccountCreatedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Account);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
