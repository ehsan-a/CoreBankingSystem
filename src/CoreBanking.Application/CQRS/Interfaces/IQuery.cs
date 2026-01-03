using MediatR;

namespace CoreBanking.Application.CQRS.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
