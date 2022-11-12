using BenchmarkDotNet.Attributes;
using QueryConverter.Core.Utils;
using QueryConverter.Presentation.Handlers;

namespace QueryConverter.Benchmarks.Handlers.Commands
{
    public class SelectBenchmark
    {
        public const string SelectWithFilters = "SELECT Name, Type, State, Pin FROM Cities WHERE Name = 'Miami' AND State = 'FL' AND ZipCodes IN(33126, 33151) AND AverageAge BETWEEN 34 AND 65AND AverageSalary >= 55230 AND AverageTemperature< 80";

        private readonly static ICondition condition = new Condition();

        private readonly static SelectCommandHandler _handler = new SelectCommandHandler(condition);

        [Benchmark]
        public void handler()
        {
            var command = new SelectCommand() { SQLQuery = SelectWithFilters };
            _handler.HandleAsync(command);
        }
    }
}
