using QueryConverter.Shared.Dto;
using TSQL.Statements;

namespace QueryConverter.Core.Convension.Handlers
{
    public interface IQueryHandler
    {
        Task<ResultModel> HandleSelectStatement(TSQLSelectStatement statement);
        Task<ResultModel> HandleGroupByStatement(TSQLSelectStatement statement);
        Task<List<string>> GetConditionStatement(List<WhereCondition> conditions);
    }
}
