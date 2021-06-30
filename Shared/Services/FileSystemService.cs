using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class FileSystemService : IFileSystemService
    {
        public async Task DeleteAsync(string path)
        {
            File.Delete(path);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(string path) => 
            await Task.FromResult(File.Exists(path));

        public async Task<IEnumerable<string>> ReadAllLinesAsync(string path) => 
            await File.ReadAllLinesAsync(path);
    }
}
