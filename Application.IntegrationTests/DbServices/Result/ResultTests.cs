using Application.Services;
using Application.Services.DbService.Result.Commands;
using Application.Services.DbService.Result.Queries;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.DbServices.Result
{
    using static Testing;
    public class ResultTests
    {
        private readonly PriceDataService _sut;
        private readonly Mock<IMediator> _mockMediator;
        public ResultTests()
        {
            _mockMediator = new Mock<IMediator>();
            _sut = new PriceDataService(_mockMediator.Object);
        }

        [Test]
        public async Task Result_Commands_Test()
        {
            var priceDataResult = new List<Domain.Entities.PriceData>()
            {
                new Domain.Entities.PriceData
                {
                    Date = System.DateTime.Now,
                    OpeningPrice = 2.5,
                    ClosingPrice = 5
                }
            };

            var results = new List<Domain.Entities.Result>();
            var result = await _sut.ParseFinalResultAsync(priceDataResult);
            results.Add(result);

            var created = await SendAsync(new CreateResultCommand { Result = results });
            var deleted = await SendAsync(new DeleteResultCommand());

            created.Should().BeTrue();
            deleted.Should().BeTrue();
        }

        [Test]
        public async Task Result_Queries_Test()
        {
            var priceDataResult = new List<Domain.Entities.PriceData>()
            {
                new Domain.Entities.PriceData
                {
                    Date = System.DateTime.Now,
                    OpeningPrice = 2.5,
                    ClosingPrice = 5
                }
            };

            var results = new List<Domain.Entities.Result>();
            var result = await _sut.ParseFinalResultAsync(priceDataResult);
            results.Add(result);

            await SendAsync(new CreateResultCommand { Result = results });
            var resultQuery = await SendAsync(new GetResultQuery());

            resultQuery.Should().NotBeNull();
            resultQuery.Should().HaveCount(1);
        }
    }
}
