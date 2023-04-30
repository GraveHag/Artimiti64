namespace Artimiti64
{
    public interface IKeyStoreService
    {
        Task<string?> GetKey();
    }
}
