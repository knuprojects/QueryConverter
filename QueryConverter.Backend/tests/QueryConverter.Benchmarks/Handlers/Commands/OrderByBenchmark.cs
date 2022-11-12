using BenchmarkDotNet.Attributes;
using QueryConverter.Core.Utils;
using QueryConverter.Presentation.Handlers;

namespace QueryConverter.Benchmarks.Handlers.Commands
{
    public class OrderByBenchmark
    {
        public const string SelectWithOrderBy = "SELECT Name, Pin FROM Citites ORDER BY Name asc";

        private readonly static ICondition condition = new Condition();

        private readonly static OrderByCommandHandler _handler = new OrderByCommandHandler(condition);

        [Benchmark]
        public void handler()
        {
            var command = new OrderByCommand() { SQLQuery = SelectWithOrderBy };
            _handler.HandleAsync(command);
        }
    }
}
