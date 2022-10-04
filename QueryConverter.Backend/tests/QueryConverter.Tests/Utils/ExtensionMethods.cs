using TSQL;
using TSQL.Statements;

namespace QueryConverter.Tests.Utils
{
    public static class ExtensionMethods
    {
        public static TSQLSelectStatement ConvertToTSQL(string query)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(query)[0] as TSQLSelectStatement;

            return statement;
        }
    }
}
