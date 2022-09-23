using QueryConverter.Core.Convension.Processor;

namespace QueryConverter.Core.Convension
{
    public class SqlToElasticSearchConversion
    {
        private readonly IQueryProcessor _queryProcessor;

        public SqlToElasticSearchConversion(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task ProcessSqlQuery(string sqlQuery)
        {
            await _queryProcessor.ProcessSqlQuery(sqlQuery);
        }
    }
}
