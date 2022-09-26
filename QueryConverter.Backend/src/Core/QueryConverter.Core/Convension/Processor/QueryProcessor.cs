using QueryConverter.Core.Convension.Handlers;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Core.Convension.Processor
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IQueryHandler _queryHelper;

        public QueryProcessor(IQueryHandler queryHelper)
        {
            _queryHelper = queryHelper;
        }

        public async Task<string> ProcessSqlQuery(string sqlQuery)
        {
            string result;
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(sqlQuery)[0] as TSQLSelectStatement;

            if (statement.GroupBy == null)
                result = await _queryHelper.HandleSelectStatement(statement);
            else
                result = await _queryHelper.HandleGroupByStatement(statement);

            return result;
        }
    }
}
