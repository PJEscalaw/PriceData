﻿using Domain.Entities;
using Shared.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace Application.Services
{
    public class PriceDataService : IPriceDataService
    {
        public async Task ProcessPriceData()
        {
            CsvParserOptions csvParserOptions = new(skipHeader: true, fieldsSeparator: ',');
            CsvService csvMapper = new();
            CsvParser<PriceData> csvParser = new(options: csvParserOptions, mapping: csvMapper);

            var priceDataList = csvParser.ReadFromFile(@"C:\Users\79554\Downloads\PriceData\PriceData_5.csv",
                                                       Encoding.ASCII).ToList();

            //logic na dito...
            var finalResults = new List<Result>();
            var results = new List<PriceData>();

            double prevClosingPrice = 0;
            foreach (var priceData in priceDataList)
            {
                //valid entry ito...
                //-1 closing price is greater than opening price.
                //1 opening price is greater than closing price.
                //0 equals.
                var currentOpeningPrice = priceData.Result.OpeningPrice.Value;
                var status = prevClosingPrice.CompareTo(currentOpeningPrice);
                if (status is < 0 or 0) //-1 or 0 are valid entries.
                {
                    status = priceData.Result.OpeningPrice.Value.CompareTo(priceData.Result.ClosingPrice.Value);
                    if (status is < 0 or 0) //-1 or 0 are valid entries.
                    {
                        prevClosingPrice = priceData.Result.ClosingPrice.Value;
                        results.Add(priceData.Result);
                    }

                    if (status > 0) // 1 not valid entries.
                    {
                        prevClosingPrice = priceData.Result.ClosingPrice.Value;
                        if (results.Count > 0)
                        {
                            finalResults.Add(new Result
                            {
                                Buy = results.Min(x => x.Date),
                                Sell = results.Max(x => x.Date),
                                PercentGain = await GetPercentGain(results.Min(x => x.OpeningPrice).Value, results.Max(x => x.ClosingPrice).Value)
                            });
                        }

                        results.Clear();
                    }
                }
                else if (status > 0) 
                {
                    status = priceData.Result.OpeningPrice.Value.CompareTo(priceData.Result.ClosingPrice.Value);
                    if (status is < 0 or 0) //-1 or 0 valid entries.
                    {
                        prevClosingPrice = priceData.Result.ClosingPrice.Value;
                        if (results.Count > 0)
                        {
                            prevClosingPrice = priceData.Result.ClosingPrice.Value;

                            finalResults.Add(new Result
                            {
                                Buy = results.Min(x => x.Date),
                                Sell = results.Max(x => x.Date),
                                PercentGain = await GetPercentGain(results.Min(x => x.OpeningPrice).Value, results.Max(x => x.ClosingPrice).Value)
                            });
                        }
                        results.Clear();
                        results.Add(priceData.Result);
                    }
                    if (status > 0) // not valid entries...
                    {
                        if (results.Count > 0)
                        {
                            prevClosingPrice = priceData.Result.ClosingPrice.Value;

                            finalResults.Add(new Result
                            {
                                Buy = results.Min(x => x.Date),
                                Sell = results.Max(x => x.Date),
                                PercentGain = await GetPercentGain(results.Min(x => x.OpeningPrice).Value, results.Max(x => x.ClosingPrice).Value)
                            });
                        }

                        results.Clear();
                    }
                }
                
            }

            Console.WriteLine("Buy        | Sell       | Percent Gain");
            foreach (var finalResult in finalResults)
            {
                Console.WriteLine($"{finalResult.Buy.Value.ToString("MM-dd-yyyy")} | {finalResult.Sell.Value.ToString("MM-dd-yyyy")} | {finalResult.PercentGain}");
            }
        }

        public async Task<double> GetPercentGain(double openingPrice, double closingPrice)
        {
            return await Task.FromResult((closingPrice - openingPrice) / Math.Abs(openingPrice) * 100);
        }
    }
}