using Application.Services;
using System;
using System.Threading.Tasks;

namespace PriceData
{
    public class PriceData
    {
        private readonly IPriceDataService _priceDataService;

        public PriceData(IPriceDataService priceDataService)
        {
            _priceDataService = priceDataService ?? throw new ArgumentNullException(nameof(priceDataService));
        }
        public async Task RunAsync() 
        {
            Console.WriteLine("hello from price data.");
            await Task.CompletedTask;
            string path = @"";
            await _priceDataService.ProcessPriceDataAsync(path);
        }
    }
}
