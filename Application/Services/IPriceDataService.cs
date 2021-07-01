using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Application.Services
{
    public interface IPriceDataService
    {
        Task<double> AddValidFinalResultAsync(List<Result> finalResults, List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status);
        Task<double> AddNotValidFinalResultAsync(List<Result> finalResults, List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status);
        Task<double> AddValidResultAsync(List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status);
        Task<Result> ParseFinalResultAsync(List<PriceData> results);
        Task<double> GetPercentGainAsync(double openingPrice, double closingPrice);
        Task<List<CsvMappingResult<PriceData>>> ParsePriceDataCsvAsync();
        Task<IEnumerable<Result>> ProcessPriceDataAsync();

    }
}
