using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Nodes;
using System.Text;

namespace Artimiti64
{
    internal sealed class WITService : IWITService
    {
        readonly HttpClient Client = new HttpClient();

        static readonly string APIEndpoint = "https://api.wit.ai";

        internal async Task Authorize()
        {
            string accessKey = await ServiceCatalog.Mediate<IKeyStoreService>().GetKey() ?? throw new ArgumentNullException(nameof(accessKey));

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessKey);

        }

        async Task<string> Send(Func<HttpRequestMessage> request, CancellationToken token = default)
        {
            try
            {
                using HttpResponseMessage response = await Client.SendAsync(request(), token);

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
                RequestUri = new QueryBuilder(APIEndpoint)
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
