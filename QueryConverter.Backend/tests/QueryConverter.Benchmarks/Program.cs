using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Core.Handlers;
using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Core.Utils;
using QueryConverter.Core.Utils.Factories;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Shared.Utils.Extensions.Conditions;
using QueryConverter.Types.Shared.Consts;
using QueryConverter.Types.Shared.Dto;
using TSQL;
using TSQL.Statements;




BenchmarkRunner.Run<HandlerBenchmark>();

public class HandlerBenchmark
{
    public const string SelectWithOrderBy = "SELECT Name, Pin FROM Citites ORDER BY Name asc"; 

    private readonly static ICondition condition = new Condition();

    private readonly static OrderByCommandHandler _handler = new OrderByCommandHandler(condition);

    [Benchmark]
    public void handler()
    {
        var command = new OrderByCommand() { SQLQuery = SelectWithOrderBy};
        _handler.HandleAsync(command);    
    }   
}

