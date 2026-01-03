using MediatR;

namespace CoreBanking.Application.CQRS.Interfaces
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
