using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using Artimiti64Core;

namespace Artimiti64
{
    internal sealed class WITService : IWITService
    {
        readonly HttpClient client;

        QueryBuilder queryBuilder => new QueryBuilder(client.BaseAddress?.OriginalString);

        public WITService(HttpClient client)
        {
            this.client = client;
        }

        async Task<string> Send(Func<HttpRequestMessage> request, CancellationToken token = default)
        {
            try
            {
                using HttpResponseMessage response = await client.SendAsync(request(), token);

                string jsonResponse = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();

                return jsonResponse;

            }
            catch (Exception ex)
            {
                Debugger.Log(3, string.Empty, ex.Message);
                throw;
            }
        }

        public Task<string> SendRequest(string message, JsonObject contextMap, string sessionId)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = queryBuilder
                    .AppendSegment("event")
                    .AppendQueryParam("session_id", sessionId)
                    .AppendQueryParam("context_map", contextMap.ToJsonString())
                    .Build(),
                Content = new StringContent($"{{\"type\": \"message\", \"message\": \"{message}\"}}", Encoding.UTF8, "application/x-www-form-urlencoded"),
            };

            return Send(() => httpRequestMessage, CancellationToken.None);
        }
    }
}
