using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text.Json.Nodes;

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
    }
}
