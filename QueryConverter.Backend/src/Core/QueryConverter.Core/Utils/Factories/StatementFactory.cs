using TSQL.Statements;

namespace QueryConverter.Core.Utils.Factories
{
    public class StatementFactory
    {
        private readonly TSQLSelectStatement _statement;

        public StatementFactory(TSQLSelectStatement statement)
        {
            _statement = statement;
        }

        public (string, string) StatementGenerator(IStatementGeneratorFactory factory)
        {
            factory = factory ?? throw new ArgumentNullException(nameof(factory));

            return factory.GenerateStatement(_statement);
        }
    }
}
