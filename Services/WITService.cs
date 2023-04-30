using System.Diagnostics;
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

                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                return jsonResponse;

            }
            catch (Exception ex)
            {
                Debugger.Log(3, string.Empty, ex.Message);
                throw;
            }
        }

        public Task<string> SendRequest(string message)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = queryBuilder
                    .AppendSegment("message")
                    .AppendQueryParam("q", message)
                    .Build()
            };

            return Send(() => httpRequestMessage, CancellationToken.None);
        }
    }
}
