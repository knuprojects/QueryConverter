using Newtonsoft.Json;

namespace QueryConverter.Core.Helpers.Extensions;

public static class ExtensionMethods
{
    public static string PrettyJson(this string jsonString)
    {
        dynamic parsedJson = JsonConvert.DeserializeObject(jsonString);
        return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
    }

    public static bool IsCollectionType(Type type)
    {
        return type.IsArray;
    }

    public static bool IsValidElementType(Type type)
    {
        return type != null && (
            type == typeof(int) ||
            type == typeof(uint) ||
            type == typeof(string) ||
            type == typeof(bool) ||
            type.IsEnum);
    }
}
