using AutoMapper;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Customers;
using CoreBanking.Domain.Events.Transactions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace CoreBanking.Application.EventHandlers.Customers
{
    public class CustomerCreatedAuditHandler : IDomainEventHandler<CustomerCreatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public CustomerCreatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(CustomerCreatedEvent domainEvent)
        {
            var auditLog = _mapper.Map<AuditLog>(domainEvent.Customer);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = domainEvent.UserId.ToString();
            await _auditLogService.LogAsync(auditLog);
        }
    }
}
