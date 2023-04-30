namespace Artimiti64
{
    internal sealed class KeyStoreService : IKeyStoreService, IService
    {
        static string StoreName => "KeyStore.json";
        static string StorePath => Path.Combine(AppContext.BaseDirectory, "../../..", StoreName);

        KeyStore? _fileSecrets;

        public KeyStoreService()
        {
        }

        async Task<KeyStore?> FileSecrets()
        {
            if (_fileSecrets is not null) return _fileSecrets;

            return _fileSecrets = await LoadKeyStore();
        }

        static async Task<KeyStore?> LoadKeyStore()
        {
            IFileService fileService = ServiceCatalog.Mediate<IFileService>();

            if (!fileService.Exists(StorePath)) return default;

            return await fileService.LoadFileContent<KeyStore>(StorePath);
        }

        async Task<string?> IKeyStoreService.GetKey() 
        {

            KeyStore? keyStore = await FileSecrets();

            if (keyStore is null || keyStore?.App_Key is null) return default;
            if (keyStore.IsValid()) return keyStore.App_Key;

            return default;
        }
        
    }
}
