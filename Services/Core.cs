using System.Text.Json.Nodes;

namespace Artimiti64
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

        public static QueryBuilder AppendSegment(this QueryBuilder builder, string segment)
        {
            builder.AddSegment(segment);
            return builder;
        }

        public static QueryBuilder AppendQueryParam(this QueryBuilder builder, string argument, string value)
        {
            builder.AddQueryParam(argument, value);
            return builder;
        }
    }
}
