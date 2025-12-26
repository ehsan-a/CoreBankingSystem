using AutoMapper;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.EventHandlers.Customers
{
    public class CustomerUpdatedAuditHandler : IDomainEventHandler<CustomerUpdatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public CustomerUpdatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(CustomerUpdatedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Customer);
            auditLog.ActionType = AuditActionType.Update;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            auditLog.OldValue = domainEvent.OldValue;
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
