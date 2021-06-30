using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Services
{
    public interface IFileSystemService
    {
        Task<bool> ExistsAsync(string path);
        Task DeleteAsync(string path);
        Task<IEnumerable<string>> ReadAllLinesAsync(string path);
    }
}
