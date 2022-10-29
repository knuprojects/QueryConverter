namespace QueryConverter.Shared.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    }
}
