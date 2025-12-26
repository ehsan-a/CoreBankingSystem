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
    public class CustomerDeletedAuditHandler : IDomainEventHandler<CustomerDeletedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public CustomerDeletedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(CustomerDeletedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Customer);
            auditLog.ActionType = AuditActionType.Delete;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
