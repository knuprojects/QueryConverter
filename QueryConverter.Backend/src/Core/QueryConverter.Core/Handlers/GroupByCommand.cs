using QueryConverter.Shared.Cqrs.Commands;

namespace QueryConverter.Core.Handlers
{
    public class GroupByCommand : ICommand
    {
        public string SQLQuery { get; set; }
    }
}
