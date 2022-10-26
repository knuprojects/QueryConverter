using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Core.Processor;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Shared.Utils.Extensions;
using QueryConverter.Shared.Utils.Extensions.Conditions;
using QueryConverter.Types.Shared.Consts;
using QueryConverter.Types.Shared.Dto;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers
{
    public class SelectCommandHandler : ICommandHandler<SQLCommand>
    {
        public Task HandleAsync(SQLCommand command, CancellationToken cancellationToken = default)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(command.SQLQuery)[0] as TSQLSelectStatement;
            try
            {
                var table = statement.From.Table().Index;
                var conditions = statement.Where.Condition();
                var fields = statement.Select.Fields();

                string tableStatement = $"POST {table}/_search";

                string fieldStatement = string.Empty;

                if (fields.Count > 0 && fields[0].Column != "*")
                {
                    fieldStatement = string.Join(", ", fields.Select(x => "\"" + x.Column + "\""));
                    fieldStatement = ", \"_source\": [" + fieldStatement + "]";
                }

                List<string> conditionsList = ConditionStatement.GetConditionStatement(conditions);
                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                string jsonPortion = $@"{{
                       {conditionsStatement}
                       {fieldStatement}
                       }}".PrettyJson();

                jsonPortion = JToken.Parse(jsonPortion).ToString(Formatting.Indented);

                var elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }
    }
}
