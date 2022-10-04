using QueryConverter.Types.Shared.Dto;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers
{
    public interface IQueryHandler
    {
        Task<ResultModel> HandleSelectStatement(TSQLSelectStatement statement);
        Task<ResultModel> HandleGroupByStatement(TSQLSelectStatement statement);
        Task<ResultModel> HandleAscendingStatement(TSQLSelectStatement statement);
        Task<ResultModel> HandleDescendingStatement(TSQLSelectStatement statement);
    }
}
