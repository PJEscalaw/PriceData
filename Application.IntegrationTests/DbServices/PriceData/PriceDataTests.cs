using Application.Services;
using Application.Services.DbService.PriceData.Commands;
using Application.Services.DbService.PriceData.Queries;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Application.IntegrationTests.DbServices.PriceData
{
    using static Testing;
    public class PriceDataTests
    {
        private readonly PriceDataService _sut;
        private readonly Mock<IMediator> _mockMediator;
        public PriceDataTests()
        {
            _mockMediator = new Mock<IMediator>();
            _sut = new PriceDataService(_mockMediator.Object);
        }
        [Test]
        public async Task PriceData_Commands_Tests()
        {
            var runningPath = AppDomain.CurrentDomain.BaseDirectory;
            var file = string.Format(@"{0}Resources\PriceData_5.csv", Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\")));

            var result = await _sut.ParsePriceDataCsvAsync(file);

            var created =  await SendAsync(new CreatePriceDataCommand { PriceData = result });
            var deleted = await SendAsync(new DeletePriceDataCommand());
            
            created.Should().BeTrue();
            deleted.Should().BeTrue();
        }

        [Test]
        public async Task PriceData_Queries_Tests()
        {
            var runningPath = AppDomain.CurrentDomain.BaseDirectory;
            var file = string.Format(@"{0}Resources\PriceData_5.csv", Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\")));

            var result = await _sut.ParsePriceDataCsvAsync(file);

            await SendAsync(new CreatePriceDataCommand { PriceData = result });
            
            var prices = await SendAsync(new GetPriceDataQuery());

            prices.Should().NotBeNull();
            prices.Should().HaveCount(8);

            await SendAsync(new DeletePriceDataCommand());
        }
    }
}
