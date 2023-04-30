using System.Text.Json.Nodes;

namespace Artimiti64
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCatalog.RegisterAllService();
            IWITService witService = await ServiceCatalog.Mediate<IWITServiceFactory>().Create();

            JsonObject testJson = new JsonObject()
            {
            };
            string result = await witService.SendRequest("Ugly shit",testJson, "proda5c");
            Console.WriteLine(result);

        }
    }
}