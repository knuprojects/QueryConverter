using QueryConverter.Core.Convension;
using QueryConverter.Types.Shared.Dto;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers
{
    public interface IQueryHandler
    {
        Task<ResultModel> HandleSelectStatement(TSQLSelectStatement statement);
        Task<ResultModel> HandleGroupByStatement(TSQLSelectStatement statement);
        Task<List<string>> GetConditionStatement(List<WhereCondition> conditions);
    }
}
