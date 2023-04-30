using System.IO;
using System.Threading.Tasks;

namespace Artimiti64
{
    public interface IFileService
    {
        void Load(string path);

        bool Exists(string path);

        Task<FileStream> LoadFileContent();

        Task<FileStream> LoadFileContent(string path);

        Task<T> LoadFileContent<T>(string path);

        Task SaveData(string path, object obj);

    }
}
