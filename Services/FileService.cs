namespace Artimiti64
{
    public sealed class FileService : IFileService, IService
    {
        public FileService()
        {
        }

        FileInfo? FileInfo;

        static bool Exists(string path) => File.Exists(path);

        static async Task<FileStream> ReadFile(string path) {

            if (!File.Exists(path)) throw new FileNotFoundException($"not exists on {path}");

            return await Task.Run(() =>
            {
                return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            });
        }

        void Load(string path)
        {
            if (!Exists(path)) throw new FileNotFoundException();
            FileInfo = new FileInfo(path);
        }

        void IFileService.Load(string path) => Load(path);

        bool IFileService.Exists(string path) => Exists(path);

        async Task<FileStream> IFileService.LoadFileContent()
        {
            if (FileInfo == null) throw new FileNotFoundException();
            return await ReadFile(FileInfo.FullName);
        }

        async Task<FileStream> IFileService.LoadFileContent(string path)
        {
            Load(path); return await ReadFile(path);
        }

        async Task<T> IFileService.LoadFileContent<T>(string path)
        {
            using FileStream stream = await ReadFile(path);
            stream.Position = 0;

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer,0, (int)stream.Length);

            string jsonString = System.Text.Encoding.UTF8.GetString(buffer);

            return Core.Deserialize<T>(jsonString);
        }

        static string Serialize(object obj) => System.Text.Json.JsonSerializer.Serialize(obj);


        async Task IFileService.SaveData(string path, object obj)
        {
            string jsonString = Serialize(obj);

            using StreamWriter sw = new StreamWriter(path);
            await sw.WriteAsync(jsonString);

        }
    }
}
