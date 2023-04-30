namespace Artimiti64
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCatalog.RegisterAllService();


            IWITService witService = await ServiceCatalog.Mediate<IWITServiceFactory>().Create();


        }
    }
}