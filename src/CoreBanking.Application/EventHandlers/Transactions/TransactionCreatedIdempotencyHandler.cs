using AutoMapper;
using CoreBanking.Application.DTOs.Responses.Transaction;
using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Enums;
using CoreBanking.Domain.Events.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.EventHandlers.Transactions
{
    public class TransactionCreatedIdempotencyHandler : IDomainEventHandler<TransactionCreatedEvent>
    {
        private readonly IIdempotencyService _idempotencyService;
        private readonly IMapper _mapper;

        public TransactionCreatedIdempotencyHandler(IIdempotencyService idempotencyService, IMapper mapper)
        {
            _idempotencyService = idempotencyService;
            _mapper = mapper;
        }

        public async Task Handle(TransactionCreatedEvent domainEvent)
        {
            await _idempotencyService.SaveResultAsync(domainEvent.IdempotencyKey,
                                                      _mapper.Map<TransactionResponseDto>(domainEvent.Transaction),
                                                      domainEvent.UserId);
        }
    }
}
