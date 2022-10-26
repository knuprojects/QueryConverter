using QueryConverter.Shared.Utils.Extensions;
using QueryConverter.Types.Shared.Consts;
using TSQL.Statements;

namespace QueryConverter.Core.Utils.Factories
{
    public interface IStatementGeneratorFactory
    {
        (string, string) GenerateStatement(TSQLSelectStatement statement);
    }

    public class SelectStatementGenerator : IStatementGeneratorFactory
    {
        public (string, string) GenerateStatement(TSQLSelectStatement statement)
        {
            var table = statement.From.Table().Index;
            var fields = statement.Select.Fields();

            string tableStatement = $"POST {table}/_search";

            string operationStatement = string.Empty;

            if (fields.Count > 0 && fields[0].Column != "*")
            {
                operationStatement = string.Join(", ", fields.Select(x => "\"" + x.Column + "\""));
                operationStatement = ", \"_source\": [" + operationStatement + "]";
            }

            return (tableStatement, operationStatement);
        }
    }

    public class OperationByStatementGenerator : IStatementGeneratorFactory
    {
        public (string, string) GenerateStatement(TSQLSelectStatement statement)
        {
            var table = statement.From.Table().Index;
            var fields = statement.Select.Fields();

            string tableStatement = $"GET {table}/_search";

            string operationStatement = string.Empty;
            const string nextAggregationMarker = "(addNextAggregationHere)";

            foreach (var field in fields)
            {
                string template = Templates.ConditionBy.Replace("(column)", field.Column);

                if (field == fields.Last())
                    template = template.Replace("(additionalAggregation)", "");
                else
                    template = template.Replace("(additionalAggregation)", nextAggregationMarker);

                if (operationStatement.Contains(nextAggregationMarker))
                    operationStatement = operationStatement.Replace(nextAggregationMarker, "," + Environment.NewLine + template);
                else
                    operationStatement = template;
            }

            return (tableStatement, operationStatement);
        }
    }
}
