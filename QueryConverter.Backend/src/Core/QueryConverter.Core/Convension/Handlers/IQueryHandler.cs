using TSQL.Statements;

namespace QueryConverter.Core.Convension.Handlers
{
    public interface IQueryHandler
    {
        Task<string> HandleSelectStatement(TSQLSelectStatement statement);
        Task<string> HandleGroupByStatement(TSQLSelectStatement statement);
        Task<List<string>> GetConditionStatement(List<WhereCondition> conditions);
    }
}
