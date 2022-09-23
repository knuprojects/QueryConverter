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

        public async Task ProcessSqlQuery(string sqlQuery)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(sqlQuery)[0] as TSQLSelectStatement;

            if (statement.GroupBy == null)
                await _queryHelper.HandleSelectStatement(statement);
            else
                await _queryHelper.HandleGroupByStatement(statement);
        }
    }
}
