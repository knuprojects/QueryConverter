using BenchmarkDotNet.Running;
using QueryConverter.Benchmarks.Handlers.Commands;



BenchmarkRunner.Run<SelectBenchmark>();
//BenchmarkRunner.Run<OrderByBenchmark>();
//BenchmarkRunner.Run<GroupByBenchmark>();
