using Shared.Services;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PriceDataService : IPriceDataService
    {
        private readonly IFileSystemService _fileSystemService;

        public PriceDataService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }
        public async Task ReadCsv() 
        {
            string path = @"C:\Users\79554\Downloads\PriceData\PriceData_1.csv";
            var lines = await _fileSystemService.ReadAllLinesAsync(path);
        }
    }
}
