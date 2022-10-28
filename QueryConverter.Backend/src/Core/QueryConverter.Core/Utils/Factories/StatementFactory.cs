using TSQL.Statements;

namespace QueryConverter.Core.Utils.Factories
{
    public class StatementFactory
    {
        public (string, string) StatementGenerator(IStatementGeneratorFactory factory, TSQLSelectStatement statement)
        {
            factory = factory ?? throw new ArgumentNullException(nameof(factory));

            return factory.GenerateStatement(statement);
        }
    }
}
