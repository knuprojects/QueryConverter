using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Shared.Cqrs.Dispatchers
{
    public interface IDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
        Task<TResult> SendAsync<TResult>(ICommand<TResult> query, CancellationToken cancellationToken = default);
    }
}
