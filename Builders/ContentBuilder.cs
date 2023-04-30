using System.Text;

namespace Artimiti64
{
    public static class ContentBuilder
    {
        public static StringContent Build(object @object) => new StringContent(System.Text.Json.JsonSerializer.Serialize(@object), Encoding.UTF8, "application/json");
    }
}
