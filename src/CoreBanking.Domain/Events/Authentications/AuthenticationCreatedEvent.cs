using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Interfaces;

namespace CoreBanking.Domain.Events.Authentications
{
    public class AuthenticationCreatedEvent : IDomainEvent
    {
        public Authentication Authentication { get; }
        public Guid UserId { get; }
        public DateTime OccurredOn { get; } = DateTime.Now;

        public AuthenticationCreatedEvent(Authentication authentication, Guid userId)
        {
            Authentication = authentication;
            UserId = userId;
        }
    }
}
