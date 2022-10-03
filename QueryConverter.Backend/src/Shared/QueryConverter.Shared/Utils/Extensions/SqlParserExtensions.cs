using QueryConverter.Core.Convension;
using QueryConverter.Shared.Types.Convension;
using QueryConverter.Types.Shared.Enums;
using TSQL.Clauses;
using TSQL.Tokens;

namespace QueryConverter.Shared.Utils.Extensions;

public static class SqlParserExtensions
{
    public static List<SelectItem> Fields(this TSQLSelectClause selectClause)
    {
        return selectClause.Tokens
            .Where(t => t.Type == TSQLTokenType.Identifier)
            .Select(f => new SelectItem() { Column = f.Text.ToString() }).ToList();
    }

    public static List<FromItem> Froms(this TSQLFromClause fromClause)
    {
        return fromClause.Tokens
            .Where(t => t.Type == TSQLTokenType.Identifier)
            .Select(f => new FromItem() { Index = f.Text.ToString() }).ToList();
    }

    public static FromItem Table(this TSQLFromClause fromClause)
    {
        return fromClause.Tokens
            .Where(t => t.Type == TSQLTokenType.Identifier)
            .Select(f => new FromItem() { Index = f.Text.ToString() }).Single();
    }

    public static List<WhereCondition> Conditions(this TSQLWhereClause whereClause)
    {
        OperatorType operatorType;

        var conditions = new List<WhereCondition>();

        WhereCondition currentCondition = null;

        if (whereClause == null)
        {
            return conditions;
        }

        foreach (TSQLToken token in whereClause.Tokens)
        {

            switch (token.Type)
            {
                case TSQLTokenType.Identifier:
                    if (currentCondition != null)
                    {
                        conditions.Add(currentCondition);
                    }

                    currentCondition = new WhereCondition() { Column = token.Text.ToString() };
                    break;
                case TSQLTokenType.Operator:
                    operatorType = WhereCondition.ToOperatorType(token.Text.ToString());
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
                    operatorType = WhereCondition.ToOperatorType(token.Text.ToString());
                    if (operatorType != OperatorType.Unknown)
                    {
                        currentCondition.Operator = operatorType;
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
