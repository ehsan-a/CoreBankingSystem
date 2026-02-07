using CoreBanking.Domain.Abstracttion;
using CoreBanking.Infrastructure.Persistence;
using MediatR;

namespace CoreBanking.Infrastructure.Extensions
{
    static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, CoreBankingContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}