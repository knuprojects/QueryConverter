using QueryConverter.Shared.Types.Convension;
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
}
