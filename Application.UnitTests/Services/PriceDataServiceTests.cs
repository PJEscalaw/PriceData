using Application.Services;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace Application.UnitTests.Services
{
    public class PriceDataServiceTests
    {
        private readonly PriceDataService _sut;

        public PriceDataServiceTests()
        {
            _sut = new PriceDataService();
        }
        [Test]
        public async Task PriceDataService_ParsePriceDataCsvAsync_Tests() 
        {
            var runningPath = AppDomain.CurrentDomain.BaseDirectory;
            var file = string.Format(@"{0}Resources\PriceData_5.csv", Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\")));
           
            var result = await _sut.ParsePriceDataCsvAsync(file);

            result.Should().NotBeNull();
            result.Should().HaveCount(8);
            result.Should().BeOfType<List<CsvMappingResult<PriceData>>>();
        }

        [Test]
        public async Task ProductDataService_ProcessPriceDataAsync_Tests() 
        {
            var runningPath = AppDomain.CurrentDomain.BaseDirectory;
            var file = string.Format(@"{0}Resources\PriceData_5.csv", Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\")));
        
            var result = await _sut.ProcessPriceDataAsync(file);

            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeOfType<List<Result>>();
            result.ElementAt(0).PercentGained.Should().Be(6.896551724137924);
        }

        [Test]
        public async Task ProductDataService_GetPercentGainAsync_ShouldCalculate() 
        {
            var result = await _sut.GetPercentGainAsync(2.5, 5);

            result.Should().Be(100);
        }

        [Test]
        public async Task ProductDataService_ParseFinalResultAsync_ShouldParseResult() 
        {
            var priceDataResult = new List<PriceData>()
            {
                new PriceData
                {
                    Date = System.DateTime.Now,
                    OpeningPrice = 2.5,
                    ClosingPrice = 5
                }
            };

            var result = await _sut.ParseFinalResultAsync(priceDataResult);

            result.PercentGained.Should().Be(100);
            result.Should().BeOfType<Result>();
        }
    }
}
