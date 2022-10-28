using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Queries;

namespace QueryConverter.Shared.Cqrs.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
             => _queryDispatcher.QueryAsync(query, cancellationToken);

        public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
            =>  _commandDispatcher.SendAsync(command, cancellationToken);
    }
}
