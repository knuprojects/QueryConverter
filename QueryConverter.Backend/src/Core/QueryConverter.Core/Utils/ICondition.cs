using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QueryConverter.Shared.Utils.Extensions;
using QueryConverter.Types.Shared.Dto;

namespace QueryConverter.Core.Utils
{
    public interface ICondition
    {
        ResultModel QueryPortion(string tableStatement, string? sizeStatement, string operationStatement, string conditionsStatement);
    }

    public class Condition : ICondition
    {
        public ResultModel QueryPortion(string tableStatement, string? sizeStatement, string operationStatement, string conditionsStatement)
        {
            string jsonPortion;

            if (sizeStatement is null)
            {
                jsonPortion = $@"{{
                       {conditionsStatement}
                       {operationStatement}
                       }}".PrettyJson();

                jsonPortion = JToken.Parse(jsonPortion).ToString(Formatting.Indented);

                var elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return result;
            }
            else
            {
                jsonPortion = $@"{{
                       {sizeStatement},
                       {operationStatement},
                       {conditionsStatement}
                       }}".PrettyJson();

                var elasticQuery = $"{tableStatement}{Environment.NewLine}{jsonPortion}";

                var rows = ExtensionMethods.SplitQuery(ref elasticQuery);

                var result = new ResultModel()
                {
                    ElasticQuery = elasticQuery,
                    Rows = rows
                };

                return result;
            }
        }
    }
}
