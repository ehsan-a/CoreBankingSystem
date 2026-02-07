using CoreBanking.Domain.Entities;
using MediatR;

namespace CoreBanking.Domain.Events.Authentications
{
    public class AuthenticationCreatedEvent : INotification
    {
        public Authentication Authentication { get; }
        public Guid UserId { get; }

        public AuthenticationCreatedEvent(Authentication authentication, Guid userId)
        {
            Authentication = authentication;
            UserId = userId;
        }
    }
}