using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Shared.Cqrs.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}
