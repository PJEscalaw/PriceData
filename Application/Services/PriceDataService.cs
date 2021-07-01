using Application.Enums;
using Domain.Entities;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace Application.Services
{
    public class PriceDataService : IPriceDataService
    {
        public async Task<IEnumerable<Result>> ProcessPriceDataAsync()
        {
            var finalResults = new List<Result>();
            var results = new List<PriceData>();
            var priceDataList = await ParsePriceDataCsvAsync();

            double prevClosingPrice = 0;
            foreach (var priceData in priceDataList)
            {
                var currentOpeningPrice = priceData.Result.OpeningPrice.Value;
                var status = prevClosingPrice.CompareTo(currentOpeningPrice);
                if (status <= (int)StatusType.Valid)
                {
                    status = priceData.Result.OpeningPrice.Value.CompareTo(priceData.Result.ClosingPrice.Value);
                    prevClosingPrice = await AddValidResultAsync(results, prevClosingPrice, priceData, status);
                    prevClosingPrice = await AddNotValidFinalResultAsync(finalResults, results, prevClosingPrice, priceData, status);
                }

                if (status == (int)StatusType.NotValid)
                {
                    status = priceData.Result.OpeningPrice.Value.CompareTo(priceData.Result.ClosingPrice.Value);
                    prevClosingPrice = await AddValidFinalResultAsync(finalResults, results, prevClosingPrice, priceData, status);
                }
            }

            return finalResults;
        }
        public async Task<double> AddValidFinalResultAsync(List<Result> finalResults, List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status)
        {
            if (status <= (int)StatusType.Valid)
            {
                if (results.Count > 0) //*
                {
                    prevClosingPrice = priceData.Result.ClosingPrice.Value;
                    finalResults.Add(await ParseFinalResultAsync(results));
                }
                results.Clear();
                results.Add(priceData.Result);
            }

            return prevClosingPrice;
        }
        public async Task<double> AddNotValidFinalResultAsync(List<Result> finalResults, List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status)
        {
            if (status == (int)StatusType.NotValid)
            {
                if (results.Count > 0)
                {
                    prevClosingPrice = priceData.Result.ClosingPrice.Value;
                    finalResults.Add(await ParseFinalResultAsync(results));
                }

                results.Clear();
            }

            return prevClosingPrice;
        }
        public async Task<double> AddValidResultAsync(List<PriceData> results, double prevClosingPrice, CsvMappingResult<PriceData> priceData, int status)
        {
            if (status <= (int)StatusType.Valid)
            {
                prevClosingPrice = priceData.Result.ClosingPrice.Value;
                results.Add(priceData.Result);
            }

            return await Task.FromResult(prevClosingPrice);
        }
        public async Task<Result> ParseFinalResultAsync(List<PriceData> results)
        {
            return new Result
            {
                Buy = results.Min(x => x.Date),
                Sell = results.Max(x => x.Date),
                PercentGained = await GetPercentGainAsync(results.Min(x => x.OpeningPrice).Value, results.Max(x => x.ClosingPrice).Value)
            };
        }
        public async Task<List<CsvMappingResult<PriceData>>> ParsePriceDataCsvAsync()
        {
            CsvParserOptions csvParserOptions = new(skipHeader: true, fieldsSeparator: ',');
            CsvService csvMapper = new();
            CsvParser<PriceData> csvParser = new(options: csvParserOptions, mapping: csvMapper);

            var priceDataList = csvParser.ReadFromFile(@"C:\Users\79554\Downloads\PriceData\PriceData_5.csv",
                                                       Encoding.ASCII).ToList();
            return await Task.FromResult(priceDataList);
        }
        public async Task<double> GetPercentGainAsync(double openingPrice, double closingPrice) => 
            await Task.FromResult((closingPrice - openingPrice) / Math.Abs(openingPrice) * 100);
    }
}
