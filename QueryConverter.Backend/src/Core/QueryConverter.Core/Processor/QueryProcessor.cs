using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Core.Handlers;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Types.Shared.Dto;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Core.Processor
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IQueryHandler _queryHelper;
        private ResultModel result;

        public QueryProcessor(IQueryHandler queryHelper)
        {
            _queryHelper = queryHelper;
        }

        public async Task<ResultModel> ProcessSqlQuery(string sqlQuery)
        {
            try
            {
                TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(sqlQuery)[0] as TSQLSelectStatement;

                if(statement.Select is not null)
                    result = await _queryHelper.HandleSelectStatement(statement);
                else if(statement.GroupBy is not null)
                    result = await _queryHelper.HandleGroupByStatement(statement);
                else if(statement.OrderBy is not null)
                    result = await _queryHelper.HandleOrderByStatement(statement);

                return result;
            }
            catch (NullReferenceException ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }
    }
}
