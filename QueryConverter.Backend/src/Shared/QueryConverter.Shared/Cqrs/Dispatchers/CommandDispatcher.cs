using Microsoft.Extensions.DependencyInjection;
using QueryConverter.Shared.Cqrs.Commands;

namespace QueryConverter.Shared.Cqrs.Dispatchers
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceProvider.CreateScope();

            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));

            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.HandleAsync));

            if (method is null)
            {
                throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
            }

            return await (Task<TResult>)method.Invoke(handler, new object[] { command, cancellationToken });
        }
    }
}
