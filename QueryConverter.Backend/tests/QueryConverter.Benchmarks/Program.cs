using BenchmarkDotNet.Running;
using QueryConverter.Benchmarks.Handlers.Commands;



//BenchmarkRunner.Run<OrderByBenchmark>();
//BenchmarkRunner.Run<SelectBenchmark>();
BenchmarkRunner.Run<GroupByBenchmark>();
