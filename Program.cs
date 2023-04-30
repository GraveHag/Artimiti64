using Artimiti64Core;

namespace Artimiti64
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ArtimitiCore.RegisterAllServices();

            IPasswordSecret secret = await ServiceCatalog.Mediate<IKeyStoreService>().GetSecret("wit_key");

            HttpClient client = ArtimitiCore.CreateInstance(builder =>
            {
                builder.AuthorizationOption = new BearerTokenOption(secret);
                builder.Url = "https://api.wit.ai";
                
            });


            IWITService witService = new WITService(client);
            string result = await witService.SendRequest("Your hair is beautiful.");
            Console.WriteLine(result);

        }
    }
}