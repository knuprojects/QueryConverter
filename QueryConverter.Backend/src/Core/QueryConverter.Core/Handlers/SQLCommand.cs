using QueryConverter.Shared.Cqrs.Commands;

namespace QueryConverter.Core.Handlers
{
    public class SQLCommand : ICommand
    {
        public string SQLQuery { get; set; }
    }
}
