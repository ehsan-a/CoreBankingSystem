using MediatR;

namespace CoreBanking.Application.CQRS.Interfaces
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
     where TQuery : IQuery<TResult>
    {
    }
}
