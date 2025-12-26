using CoreBanking.Application.Interfaces;
using CoreBanking.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Infrastructure.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
           
            var eventType = domainEvent.GetType();

         
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

          
            var handlers = (IEnumerable<object>)_serviceProvider.GetServices(handlerType);

            
            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                if (method != null)
                    await (Task)method.Invoke(handler, new object[] { domainEvent });
            }
        }
    }


}
