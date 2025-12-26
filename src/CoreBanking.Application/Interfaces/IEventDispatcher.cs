using CoreBanking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Interfaces
{
    public interface IEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
    }

}
