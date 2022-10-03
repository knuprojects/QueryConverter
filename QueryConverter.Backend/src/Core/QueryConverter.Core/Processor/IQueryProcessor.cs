using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Core.Processor
{
    public interface IQueryProcessor
    {
        Task<ResultModel> ProcessSqlQuery(string sqlQuery);
    }
}
