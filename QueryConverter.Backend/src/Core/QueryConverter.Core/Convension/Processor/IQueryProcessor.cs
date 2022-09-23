namespace QueryConverter.Core.Convension.Processor
{
    public interface IQueryProcessor
    {
        Task ProcessSqlQuery(string sqlQuery);
    }
}
