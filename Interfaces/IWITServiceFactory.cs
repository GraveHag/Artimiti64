namespace Artimiti64
{
    public interface IWITServiceFactory
    {
        Task<IWITService> Create();
    }
}
