using QueryConverter.Shared.Types.Convension;
using QueryConverter.Types.Shared.Consts;
using QueryConverter.Types.Shared.Enums;

namespace QueryConverter.Core.Processor
{
    public static class ConditionStatement
    {
        public static List<string> GetConditionStatement(List<GenericCondition> conditions)
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
                    case OperatorType.NotEqual:
                    case OperatorType.LessThan:
                    case OperatorType.LessThanOrEquals:
                        conditionText = Templates.ComparisonCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(operator)", GenericCondition.FromOperatorType(condition.Operator))
                                                    .Replace("(value)", condition.SingularValue);
                        break;
                    case OperatorType.Like:
                        conditionText = Templates.LikeCondition
                                                    .Replace("(column)", condition.Column)
                                                    .Replace("(value)", condition.SingularValue.Replace("%", "*").ToLower());
                        break;
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
                    case OperatorType.Unknown:
                        break;
                }

                conditionsList.Add(conditionText);
            }

            return conditionsList;
        }
    }
}
