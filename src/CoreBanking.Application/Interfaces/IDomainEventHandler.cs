using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }

}
