using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Core.Utils;
using QueryConverter.Core.Utils.Factories;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Shared.Utils.Extensions.Conditions;
using QueryConverter.Types.Shared.Consts;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers.Commands
{
    public class SelectCommandHandler : ICommandHandler<SelectCommand>
    {
        private readonly ICondition _condition;

        public SelectCommandHandler(Condition condition)
        {
            _condition = condition;
        }

        public Task HandleAsync(SelectCommand command, CancellationToken cancellationToken = default)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(command.SQLQuery)[0] as TSQLSelectStatement;

            var factory = new StatementFactory(statement);

            try
            {
                var conditions = statement.Where.Condition();

                var statementGenerator = factory.StatementGenerator(new SelectStatementGenerator());

                List<string> conditionsList = ConditionStatement.GetConditionStatement(conditions);

                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                var result = _condition.QueryPortion(statementGenerator.Item1, null, statementGenerator.Item2, conditionsStatement);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }
    }
}
