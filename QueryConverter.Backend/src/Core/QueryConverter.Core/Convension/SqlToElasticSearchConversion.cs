using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryConverter.Core.Enums;
using QueryConverter.Core.Helpers.Extensions;
using TSQL;
using TSQL.Statements;

namespace QueryConverter.Core.Convension
{
    public class SqlToElasticSearchConversion
    {
        public SqlToElasticSearchConversion(string sqlQuery)
        {
            ProcessSqlQuery(sqlQuery);
        }

        private void ProcessSqlQuery(string sqlQuery)
        {
            TSQLSelectStatement statement = TSQLStatementReader.ParseStatements(sqlQuery)[0] as TSQLSelectStatement;

            if (statement.GroupBy == null)
            {
                HandleSelectStatement(statement);
            }
            else
            {
                HandleGroupByStatement(statement);
            }
        }

        private void HandleGroupByStatement(TSQLSelectStatement statement)
        {

            var table = statement.From.Table().Index;
            var conditions = statement.Where.Conditions();
            var fields = statement.Select.Fields();

            string tableStatement = $"GET {table}/_search";

            #region Build Group By Statement
            string groupByStatement = string.Empty;
            const string nextAggregationMarker = "(addNextAggregationHere)";

            foreach (var field in fields)
            {
                string template = Templates.GroupBy.Replace("(column)", field.Column);

                if (field == fields.Last())
                {
                    template = template.Replace("(additionalAggregation)", "");
                }
                else
                {
                    template = template.Replace("(additionalAggregation)", nextAggregationMarker);
                }

                if (groupByStatement.Contains(nextAggregationMarker))
                {
                    groupByStatement = groupByStatement.Replace(nextAggregationMarker, "," + Environment.NewLine + template);
                }
                else
                {
                    groupByStatement = template;
                }
            }
            #endregion

            List<string> conditionsList = GetConditionStatement(conditions);
            string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

            string sizeStatement = Templates.SizeZero;

            string jsonPortion = $@"{{
                {sizeStatement},
                {groupByStatement},
                {conditionsStatement}
            }}".PrettyJson();

            // set module level variable
            ElasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";
        }

        private void HandleSelectStatement(TSQLSelectStatement statement)
        {
            var table = statement.From.Table().Index;
            var conditions = statement.Where.Conditions();
            var fields = statement.Select.Fields();

            // get table statement
            string tableStatement = $"POST {table}/_search";

            // get the field statement
            string fieldStatement = string.Empty;

            if (fields.Count > 0 && fields[0].Column != "*")
            {
                // add quotes around each field, plus a starting minus
                fieldStatement = string.Join(", ", fields.Select(x => "\"" + x.Column + "\""));
                fieldStatement = ", \"_source\": [" + fieldStatement + "]";
            }

            List<string> conditionsList = GetConditionStatement(conditions);
            string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

            string jsonPortion = $@"{{
                {conditionsStatement}
                {fieldStatement}
            }}";

            // format JSON
            jsonPortion = JToken.Parse(jsonPortion).ToString(Formatting.Indented);

            // set module level variable
            ElasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";
        }

        private static List<string> GetConditionStatement(List<WhereCondition> conditions)
        {
            // get the conditions statement
            string conditionText = string.Empty;
            var conditionsList = new List<string>();

            foreach (var condition in conditions)
            {

                switch (condition.Operator)
                {
                    case OperatorType.Equal:

                        switch (condition.Type)
                        {

                            case LiteralType.Numeric:
                            case LiteralType.String:
                                conditionText = Templates.SingleCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(value)", condition.SingularValue);
                                break;
                        }
                        break;
                    case OperatorType.In:

                        // add switch for condition types later
                        conditionText = Templates.InCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(value)", string.Join(",", condition.InValues.Select(x => "\"" + x + "\"")));

                        break;

                    case OperatorType.Between:

                        // add switch for condition types later
                        conditionText = Templates.BetweenCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(lowerValue)", condition.BetweenValues.First())
                                                    .Replace("(upperValue)", condition.BetweenValues.Last());
                        break;

                    case OperatorType.GreaterThan:
                    case OperatorType.GreaterThanOrEquals:
                    case OperatorType.LessThan:
                    case OperatorType.LessThanOrEquals:
                        // add switch for condition types later
                        conditionText = Templates.ComparisonCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(operator)", WhereCondition.FromOperatorType(condition.Operator))
                                                    .Replace("(value)", condition.SingularValue);
                        break;

                    case OperatorType.Like:
                        conditionText = Templates.LikeCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(value)", condition.SingularValue.Replace("%", "*").ToLower());

                        break;

                    case OperatorType.Unknown:
                        break;
                }

                conditionsList.Add(conditionText);
            }

            return conditionsList;
        }

        public string ElasticQuery { get; set; } = string.Empty;
    }
}
