using TSQL.Statements;

namespace QueryConverter.Core.Utils.Strategies
{
    public class StatementFactory
    {
        public (string, string) StatementGenerator(IStatementGeneratorStrategy strategy, TSQLSelectStatement statement)
        {
            strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

            return strategy.GenerateStatement(statement);
        }
    }
}
