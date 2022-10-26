using QueryConverter.Shared.Cqrs.Commands;

namespace QueryConverter.Core.Handlers
{
    public class OrderByCommand : ICommand
    {
        public string SQLQuery { get; set; }
    }
}
