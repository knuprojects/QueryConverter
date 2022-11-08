using BenchmarkDotNet.Attributes;
using QueryConverter.Core.Handlers;
using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Core.Utils;

namespace QueryConverter.Benchmarks.Handlers.Commands
{
    public class GroupByBenchmark
    {
        public const string SelectWithFilterAndGroupBy = "SELECT SolarSystem, Galaxy FROM Planets WHERE SpacecraftWithinKilometers< 10000 GROUP BY SolarSystem, Galaxy";

        private readonly static ICondition condition = new Condition();

        private readonly static GroupByCommandHandler _handler = new GroupByCommandHandler(condition);

        [Benchmark]
        public void handler()
        {
            var command = new GroupByCommand() { SQLQuery = SelectWithFilterAndGroupBy };
            _handler.HandleAsync(command);
        }
    }
}
