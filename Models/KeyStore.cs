namespace Artimiti64
{
    public class KeyStore
    {
        public string? App_Key { get; set; }
        public string? App_Id { get; set; }

        public bool IsValid() => !(string.IsNullOrEmpty(App_Key) && string.IsNullOrEmpty(App_Id));
    }
}
