namespace Artimiti64
{
    internal sealed class WITFactory : IWITServiceFactory, IService
    {
        async Task<IWITService> IWITServiceFactory.Create()
        {
            WITService witService = new WITService();

            await witService.Authorize();

            return witService;
        }
    }
}
