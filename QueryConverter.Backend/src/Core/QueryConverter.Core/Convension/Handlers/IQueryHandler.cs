using TSQL.Statements;

namespace QueryConverter.Core.Convension.Handlers
{
    public interface IQueryHandler
    {
        Task HandleSelectStatement(TSQLSelectStatement statement);
        Task HandleGroupByStatement(TSQLSelectStatement statement);
        Task<List<string>> GetConditionStatement(List<WhereCondition> conditions);
    }
}
