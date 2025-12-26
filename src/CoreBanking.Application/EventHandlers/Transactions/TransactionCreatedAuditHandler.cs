using AutoMapper;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CoreBanking.Application.EventHandlers.Transactions
{
    public class TransactionCreatedAuditHandler : IDomainEventHandler<TransactionCreatedEvent>
    {
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;

        public TransactionCreatedAuditHandler(IAuditLogService auditLogService, IMapper mapper)
        {
            _auditLogService = auditLogService;
            _mapper = mapper;
        }

        public async Task Handle(TransactionCreatedEvent domainEvent)
        {
            var transactionDto = _mapper.Map<TransactionResponseDto>(domainEvent.Transaction);

            var auditLog = _mapper.Map<AuditLog>(transactionDto);
            auditLog.ActionType = AuditActionType.Create;
            auditLog.PerformedBy = domainEvent.UserId.ToString();

            await _auditLogService.LogAsync(auditLog);
        }
    }


}
