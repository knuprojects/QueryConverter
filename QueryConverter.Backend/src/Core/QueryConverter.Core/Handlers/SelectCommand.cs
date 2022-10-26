using QueryConverter.Shared.Cqrs.Commands;

namespace QueryConverter.Core.Handlers
{
    public class SelectCommand : ICommand
    {
        public string SQLQuery { get; set; }
    }
}
