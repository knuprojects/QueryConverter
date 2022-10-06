using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryConverter.Core.Convension;
using QueryConverter.Core.ExceptionCodes;
using QueryConverter.Shared.Types.Convension;
using QueryConverter.Shared.Types.Exceptions;
using QueryConverter.Shared.Utils.Extensions;
using QueryConverter.Types.Shared.Consts;
using QueryConverter.Types.Shared.Dto;
using QueryConverter.Types.Shared.Enums;
using TSQL.Statements;

namespace QueryConverter.Core.Handlers
{
    public class QueryHandler : IQueryHandler
    {
        private string elasticQuery;

        public async Task<List<string>> GetConditionStatement(List<WhereCondition> conditions)
        {
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
                        conditionText = Templates.InCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(value)", string.Join(",", condition.InValues.Select(x => "\"" + x + "\"")));
                        break;

                    case OperatorType.Between:
                        conditionText = Templates.BetweenCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(lowerValue)", condition.BetweenValues.First())
                                                    .Replace("(upperValue)", condition.BetweenValues.Last());
                        break;
                    case OperatorType.GreaterThan:
                    case OperatorType.GreaterThanOrEquals:
                    case OperatorType.LessThan:
                    case OperatorType.LessThanOrEquals:
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

        public async Task<List<string>> GetOrderByConditionStatement(List<OrderByCondition> conditions)
        {

            string conditionText = string.Empty;

            var conditionsList = new List<string>();

            foreach (var condition in conditions)
            {
                switch (condition.Operator)
                {
                    case OperatorType.Descending:
                        conditionText = Templates.OrderCondition
                            .Replace("(column)", condition.Column)
                            .Replace("(operator)", condition.DescdendingValue);
                        break;
                    case OperatorType.Ascending:
                        conditionText = Templates.OrderCondition
                            .Replace("(column)", condition.Column)
                            .Replace("(operator)", condition.AscendingValue);
                        break;
                }
                conditionsList.Add(conditionText);
            }
            return conditionsList;
        }

        public async Task<ResultModel> HandleSelectStatement(TSQLSelectStatement statement)
        {
            try
            {
                var table = statement.From.Table().Index;
                var conditions = statement.Where.Conditions();
                var fields = statement.Select.Fields();

                string tableStatement = $"POST {table}/_search";

                string fieldStatement = string.Empty;

                if (fields.Count > 0 && fields[0].Column != "*")
                {
                    fieldStatement = string.Join(", ", fields.Select(x => "\"" + x.Column + "\""));
                    fieldStatement = ", \"_source\": [" + fieldStatement + "]";
                }

                List<string> conditionsList = await GetConditionStatement(conditions);
                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                string jsonPortion = $@"{{
                       {conditionsStatement}
                       {fieldStatement}
                       }}".PrettyJson();

                jsonPortion = JToken.Parse(jsonPortion).ToString(Formatting.Indented);

                elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }

        public async Task<ResultModel> HandleGroupByStatement(TSQLSelectStatement statement)
        {
            try
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
                    string template = Templates.ConditionBy.Replace("(column)", field.Column);

                    if (field == fields.Last())
                        template = template.Replace("(additionalAggregation)", "");
                    else
                        template = template.Replace("(additionalAggregation)", nextAggregationMarker);

                    if (groupByStatement.Contains(nextAggregationMarker))
                        groupByStatement = groupByStatement.Replace(nextAggregationMarker, "," + Environment.NewLine + template);
                    else
                        groupByStatement = template;
                }
                #endregion

                List<string> conditionsList = await GetConditionStatement(conditions);
                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                string sizeStatement = Templates.SizeZero;

                string jsonPortion = $@"{{
                       {sizeStatement},
                       {groupByStatement},
                       {conditionsStatement}
                       }}".PrettyJson();

                elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }

        }

        public async Task<ResultModel> HandleOrderByStatement(TSQLSelectStatement statement)
        {
            try
            {
                var table = statement.From.Table().Index;
                var conditions = statement.OrderBy.OrderByConditions();
                var fields = statement.Select.Fields();

                string tableStatement = $"GET {table}/_search";

                string orderByStatement = string.Empty;
                const string nextAggregationMarker = "(addNextAggregationHere)";

                foreach (var field in fields)
                {
                    string template = Templates.ConditionBy.Replace("(column)", field.Column);

                    if (field == fields.Last())
                        template = template.Replace("(additionalAggregation)", "");
                    else
                        template = template.Replace("(additionalAggregation)", nextAggregationMarker);

                    if (orderByStatement.Contains(nextAggregationMarker))
                        orderByStatement = orderByStatement.Replace(nextAggregationMarker, "," + Environment.NewLine + template);
                    else
                        orderByStatement = template;
                }

                List<string> conditionsList = await GetOrderByConditionStatement(conditions);
                string conditionsStatement = Templates.Conditions.Replace("(conditions)", string.Join(",", conditionsList));

                string sizeStatement = Templates.SizeZero;

                string jsonPortion = $@"{{
                       {sizeStatement},
                       {orderByStatement},
                       {conditionsStatement}
                       }}".PrettyJson();

                elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return result;

            }
            catch (Exception ex)
            {
                throw new QueryConverterException(Codes.InvalidArguments, $"{ex.Message}");
            }
        }
    }
}
