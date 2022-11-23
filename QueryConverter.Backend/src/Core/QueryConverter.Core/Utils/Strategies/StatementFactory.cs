using TSQL.Statements;

namespace QueryConverter.Core.Utils.Strategies
{
    public class StatementStrategy
    {
        public (string, string) StatementGenerator(IStatementGeneratorStrategy factory, TSQLSelectStatement statement)
        {
            factory = factory ?? throw new ArgumentNullException(nameof(factory));

            return factory.GenerateStatement(statement);
        }
    }
}
