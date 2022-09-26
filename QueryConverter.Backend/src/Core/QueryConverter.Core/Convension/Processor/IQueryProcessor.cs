namespace QueryConverter.Core.Convension.Processor
{
    public interface IQueryProcessor
    {
        Task<string> ProcessSqlQuery(string sqlQuery);
    }
}
