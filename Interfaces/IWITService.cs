namespace Artimiti64
{
    public interface IWITService
    {
        Task<string> SendRequest(string message);
    }
}
