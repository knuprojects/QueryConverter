using Newtonsoft.Json;

namespace QueryConverter.Core.Helpers
{
    public static class ExtensionMethods
    {
        public static string PrettyJson(this string jsonString)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(jsonString);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
