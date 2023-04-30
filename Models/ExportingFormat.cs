
using System.Text.Json.Serialization;

namespace Artimiti64
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExportingFormat
    {
        json = 0,
        xml = 10,
        csv = 30
    }
}
