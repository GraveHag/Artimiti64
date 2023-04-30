using System.Text.Json.Nodes;

namespace Artimiti64.Services
{
    public static class Core
    {
        public static T Deserialize<T>(string jsonString) => System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, new System.Text.Json.JsonSerializerOptions() { Converters = { new TimeSpanConverter() } }) ?? throw new Exception("Cannot deserialize");

        public static string Deserialize(string jsonString, string key)
        {
            JsonObject? @object = Deserialize<JsonObject>(jsonString);
            if (!@object.TryGetPropertyValue(key, out JsonNode? node)) throw new ArgumentException("node key is missing");

            return node!.GetValue<string>();
        }
    }
}
