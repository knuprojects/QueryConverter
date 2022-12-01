using QueryConverter.Shared.Types.Convension;
using QueryConverter.Types.Shared.Enums;
using TSQL.Clauses;
using TSQL.Tokens;

namespace QueryConverter.Core.Conditions
{
    public static class ConditionGenerator
    {
        public static List<GenericCondition> Condition(this TSQLClause clause)
        {
            OperatorType operatorType;

            var conditions = new List<GenericCondition>();

            GenericCondition currentCondition = null;

            if (clause == null)
            {
                return conditions;
            }

            foreach (TSQLToken token in clause.Tokens)
            {
                switch (token.Type)
                {
                    case TSQLTokenType.Identifier:
                        if (currentCondition != null)
                        {
                            conditions.Add(currentCondition);
                        }

                        currentCondition = new GenericCondition() { Column = token.Text.ToString() };
                        break;
                    case TSQLTokenType.Operator:
                        operatorType = GenericCondition.ToOperatorType(token.Text.ToString());
                        if (operatorType != OperatorType.Unknown)
                        {
                            currentCondition.Operator = operatorType;
                        }
                        break;
                    case TSQLTokenType.NumericLiteral:
                        currentCondition.Type = LiteralType.Numeric;
                        currentCondition.Value = token.Text.ToString();
                        break;
                    case TSQLTokenType.StringLiteral:
                        currentCondition.Type = LiteralType.String;
                        currentCondition.Value = token.Text.ToString();
                        break;
                    case TSQLTokenType.Keyword:
                        operatorType = GenericCondition.ToOperatorType(token.Text.ToString());
                        if (operatorType != OperatorType.Unknown)
                        {
                            currentCondition.Operator = operatorType;
                            currentCondition.Value = token.Text.ToString();
                        }
                        break;

                    case TSQLTokenType.MoneyLiteral:
                        break;
                    case TSQLTokenType.BinaryLiteral:
                        break;
                    case TSQLTokenType.SystemIdentifier:
                        break;
                    case TSQLTokenType.SingleLineComment:
                        break;
                    case TSQLTokenType.MultilineComment:
                        break;
                    case TSQLTokenType.Variable:
                        break;
                    case TSQLTokenType.SystemVariable:
                        break;
                    case TSQLTokenType.Whitespace:
                        break;
                    case TSQLTokenType.Character:
                        break;
                }
            }

            if (currentCondition != null)
            {
                conditions.Add(currentCondition);
            }

            return conditions;
        }
    }
}
