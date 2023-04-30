using System.Text.Json.Nodes;

namespace Artimiti64
{
    public interface IWITService
    {
        Task<string> SendRequest(string message, JsonObject contextMap, string sessionId);
    }
}
