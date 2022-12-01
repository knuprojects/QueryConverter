using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Core.Utils;
using QueryConverter.Core.Utils.Strategies;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Core.Conditions;
using QueryConverter.Types.Shared.Consts;
using QueryConverter.Types.Shared.Dto;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Presentation.Handlers
{
    public class OrderByCommandHandler : ICommandHandler<OrderByCommand, ResultModel>
    {
        private readonly ICondition _condition;

        public OrderByCommandHandler(ICondition condition)
        {
            _condition = condition;
        }

        public Task<ResultModel> HandleAsync(OrderByCommand command, CancellationToken cancellationToken = default)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(command.SQLQuery)[0] as TSQLSelectStatement;

            var strategy = new StatementStrategy();

            try
            {
                var conditions = statement.OrderBy.Condition();

                var statementGenerator = strategy.StatementGenerator(new OperationByStatementGenerator(), statement);

                List<string> conditionsList = ConditionStatement.GetConditionStatement(conditions);

                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                string sizeStatement = Templates.SizeZero;

                var result = _condition.QueryPortion(statementGenerator.Item1, sizeStatement, statementGenerator.Item2, conditionsStatement);

                return Task.FromResult(result);

            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }
    }
}
