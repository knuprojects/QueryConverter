using QueryConverter.Types.Shared.Dto;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers
{
    public interface IQueryHandler
    {
        Task<ResultModel> HandleOrderByStatement(TSQLSelectStatement statement);
    }
}
