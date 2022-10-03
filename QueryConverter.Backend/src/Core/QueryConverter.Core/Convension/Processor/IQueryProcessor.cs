using QueryConverter.Shared.Dto;

namespace QueryConverter.Core.Convension.Processor
{
    public interface IQueryProcessor
    {
        Task<ResultModel> ProcessSqlQuery(string sqlQuery);
    }
}
