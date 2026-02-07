using AutoMapper;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Customers;
using MediatR;

namespace CoreBanking.Application.EventHandlers.Customers
{
    public class CustomerCreatedAuditHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public CustomerCreatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Customer);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
